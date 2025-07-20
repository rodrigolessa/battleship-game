using frm.Infrastructure.Messaging.Configurations;
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
            return;
        }

        var factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.HostPort,
            UserName = settings.UserName,
            Password = settings.UserPassword,
            VirtualHost = "/",
            ConsumerDispatchConcurrency =
                1 // A value greater than one enables parallelism for a single consumer on a single session/channel, within the limits of the prefetchCount
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

            await CreateTheExchange(channel, channelSettings, cancellationToken);

            await CreateTheQueue(channel, channelSettings, cancellationToken);

            await AddBindBetweenExchangeAndQueue(channel, channelSettings, cancellationToken);
        }
    }

    private static async Task AddBindBetweenExchangeAndQueue(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await channel.QueueBindAsync(
            queue: channelSettings.QueueName,
            exchange: channelSettings.Name,
            routingKey: channelSettings.BindKey,
            cancellationToken: cancellationToken);
    }

    private static async Task CreateTheQueue(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await channel.QueueDeclareAsync(
            queue: channelSettings.QueueName,
            durable: channelSettings.UsePersistentStorage,
            exclusive: false,
            autoDelete: channelSettings.AutoDelete,
            cancellationToken: cancellationToken);
        
        // TODO: Set queue common arguments
        // x-message-ttl = TTL (Time-to-Live) for each message (in ms)
        // x-dead-letter-exchange = Exchange to route expired/rejected messages
        // x-dead-letter-routing-key = Routing key for DLX messages
        // x-max-priority = Enable message priorities (0–255)
        // x-queue-mode = Set lazy for disk-based queues
    }

    private static async Task CreateTheExchange(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(
            exchange: channelSettings.Name,
            type: channelSettings.Type.ToString(),
            durable: channelSettings.UsePersistentStorage,
            autoDelete: channelSettings.AutoDelete,
            cancellationToken: cancellationToken);
        
        // TODO: Set exchange common arguments
        // alternate-exchange = Fallback exchange if no queue matches
    }

    /// <summary>
    /// In RabbitMQ, prefetch size (set via BasicQos) refers to the number of bytes of unacknowledged messages allowed per consumer before RabbitMQ stops delivering more.
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