using BattleshipGame.Infrastructure.Exceptions;
using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.Enumerations;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public class RabbitMqInitializer(MessageBrokerSettings settings) : IHostedService
{
    private IConnection? _connection;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(settings.HostName) || settings.HostPort == 0 ||
            string.IsNullOrWhiteSpace(settings.UserName) || string.IsNullOrWhiteSpace(settings.UserPassword))
        {
            throw new ConfigurationErrorException("The message broker setting is missing or empty.");
        }

        var factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.HostPort,
            UserName = settings.UserName,
            Password = settings.UserPassword,
            VirtualHost = "/",
            ConsumerDispatchConcurrency = 1
            // A value greater than one enables parallelism for a single consumer on a single session/channel, within the limits of the prefetchCount
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);

        await CreateAllChannels(cancellationToken);
    }

    private async Task CreateAllChannels(CancellationToken cancellationToken)
    {
        foreach (var channelSettings in settings.Channels)
        {
            if (_connection is not { IsOpen: true }) continue;

            if (!channelSettings.EnableChannel) continue;

            var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await SetUnackedMessageLimit(channel, channelSettings, cancellationToken);

            var deadletter = await CreateTheExchange(channel, channelSettings, cancellationToken);

            await CreateAnyNecessaryQueues(channel, channelSettings, deadletter, cancellationToken);
        }
    }

    private static async Task AddBindBetweenExchangeAndQueue(
        IChannel channel,
        string exchangeName,
        MessageBrokerQueueSettings queueSettings,
        CancellationToken cancellationToken)
    {
        await channel.QueueBindAsync(
            queue: queueSettings.Name,
            exchange: exchangeName,
            routingKey: queueSettings.BindKey,
            cancellationToken: cancellationToken);
    }

    private async Task CreateAnyNecessaryQueues(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        MessageBrokerExchangeSettings deadLetterExchangeSettings,
        CancellationToken cancellationToken)
    {
        foreach (var queue in channelSettings.Queues)
        {
            // Main queue        
            await channel.QueueDeclareAsync(
                queue: queue.Name,
                durable: queue.UsePersistentStorage,
                exclusive: false,
                autoDelete: queue.AutoDelete,
                cancellationToken: cancellationToken,
                arguments: null);

            await AddBindBetweenExchangeAndQueue(channel, channelSettings.Exchange.Name, queue, cancellationToken);

            // TODO: Set queue common arguments
            // x-message-ttl = TTL (Time-to-Live) for each message (in ms)
            // x-dead-letter-exchange = Exchange to route expired/rejected messages
            // x-dead-letter-routing-key = Routing key for DLX messages
            // x-max-priority = Enable message priorities (0–255)
            // x-queue-mode = Set lazy for disk-based queues

            await CreateARetryWithDeadLetter(channel, queue, deadLetterExchangeSettings, cancellationToken);
        }
    }

    private static async Task<MessageBrokerQueueSettings> AddDeaLetterForExhaustedRetries(
        IChannel channel,
        MessageBrokerQueueSettings queueSettings,
        CancellationToken cancellationToken)
    {
        // Dead-letter queue for exhausted retries
        var deadLetterSettings = new MessageBrokerQueueSettings()
        {
            Name = $"{queueSettings.Name}.dead",
            BindKey = $"{queueSettings.BindKey}.dead",
            UsePersistentStorage = queueSettings.UsePersistentStorage,
            AutoDelete = queueSettings.AutoDelete,
            EnableDeadLetter = false
        };
        
        await channel.QueueDeclareAsync(
            queue: deadLetterSettings.Name,
            durable: deadLetterSettings.UsePersistentStorage,
            exclusive: false,
            autoDelete: deadLetterSettings.AutoDelete,
            cancellationToken: cancellationToken,
            arguments: null);

        return deadLetterSettings;
    }

    private async Task CreateARetryWithDeadLetter(
        IChannel channel,
        MessageBrokerQueueSettings queueSettings,
        MessageBrokerExchangeSettings deadLetterExchangeSettings,
        CancellationToken cancellationToken)
    {
        // Retry queue with dead-letter on expiration
        var retryQueue = $"{queueSettings.Name}.retry";
        // When you declare a queue, you can specify an alternate exchange to handle such messages:
        var retryArgs = new Dictionary<string, object?>
        {
            { "x-message-ttl", queueSettings.MessageTimeToLiveInMilliseconds }
        };

        if (queueSettings.EnableDeadLetter && !string.IsNullOrWhiteSpace(deadLetterExchangeSettings.Name))
        {
            var deadLetterQueueSettings =
                await AddDeaLetterForExhaustedRetries(channel, queueSettings, cancellationToken);

            await AddBindBetweenExchangeAndQueue(channel, deadLetterExchangeSettings.Name, deadLetterQueueSettings, cancellationToken);
            
            retryArgs.Add("x-dead-letter-exchange", deadLetterExchangeSettings.Name);
            retryArgs.Add("x-dead-letter-routing-key", deadLetterQueueSettings.BindKey);
        }

        await channel.QueueDeclareAsync(
            queue: retryQueue,
            durable: queueSettings.UsePersistentStorage,
            exclusive: false,
            autoDelete: queueSettings.AutoDelete,
            cancellationToken: cancellationToken,
            arguments: retryArgs);
    }

    public static async Task<MessageBrokerExchangeSettings> CreateTheExchange(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(
            exchange: channelSettings.Exchange.Name,
            type: channelSettings.Exchange.RoutingType.ToString().ToLowerInvariant(),
            durable: channelSettings.Exchange.UsePersistentStorage,
            autoDelete: channelSettings.Exchange.AutoDelete,
            cancellationToken: cancellationToken);

        // TODO: Set exchange common arguments
        // alternate-exchange = Fallback exchange if no queue matches

        return await CreateADeadLetterExchange(channel, channelSettings, cancellationToken);
    }

    private static async Task<MessageBrokerExchangeSettings> CreateADeadLetterExchange(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        var deadLetter = new MessageBrokerExchangeSettings()
        {
            Name = $"{channelSettings.Exchange.Name}.dead",
            RoutingType = RoutingType.Direct,
            UsePersistentStorage = channelSettings.Exchange.UsePersistentStorage,
            AutoDelete = false
        };
        await channel.ExchangeDeclareAsync(
            exchange: deadLetter.Name,
            type: deadLetter.RoutingType.ToString().ToLowerInvariant(),
            durable: deadLetter.UsePersistentStorage,
            autoDelete: deadLetter.AutoDelete,
            cancellationToken: cancellationToken);

        return deadLetter;
    }

    /// <summary>
    /// In RabbitMQ, prefetch size (set via BasicQos) refers to the number of bytes of unacknowledged messages
    /// allowed per consumer before RabbitMQ stops delivering more.
    /// How to Estimate a Good prefetchCount
    /// Instead of prefetchSize, calculate a good prefetch count based on:
    ///     prefetchCount = processingTimeInMs * throughputPerSecond
    /// Example:
    ///     Avg processing time: 500ms
    ///     Desired throughput: 10 msg/sec
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="channelSettings"></param>
    /// <param name="cancellationToken"></param>
    private static async Task SetUnackedMessageLimit(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await channel.BasicQosAsync(0, channelSettings.PrefetchCount, false, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_connection is not null)
        {
            await _connection.CloseAsync(cancellationToken: cancellationToken);
        }
    }
}