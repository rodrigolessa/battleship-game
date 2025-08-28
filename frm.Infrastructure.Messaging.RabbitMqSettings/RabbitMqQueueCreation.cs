using frm.Infrastructure.Messaging.Configurations;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public static class RabbitMqQueueCreation
{
    public static async Task CreateAnyNecessaryQueues(IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        var mainExchangeName = channelSettings.Exchange.Name;
        var retryExchangeName = RabbitMqExchangeCreation.GetRetryExchangeName(mainExchangeName);
        var deadLetterExchangeName = RabbitMqExchangeCreation.GetDeadLetterExchangeName(mainExchangeName);

        foreach (var queue in channelSettings.Queues)
        {
            
            // Exchange to route/redirect expired, rejected or failures messages
            var retryArgs = new Dictionary<string, object?>
            {
                { "x-dead-letter-exchange", deadLetterExchangeName },
                { "x-dead-letter-routing-key", queue.BindKey }
            };

            // Main Queue
            await channel.QueueDeclareAsync(
                queue: queue.Name,
                durable: queue.UsePersistentStorage,
                exclusive: false,
                autoDelete: queue.AutoDelete,
                cancellationToken: cancellationToken,
                arguments: retryArgs);

            await AddBindBetweenExchangeAndQueue(channel, mainExchangeName, queue.Name, queue.BindKey,
                cancellationToken);

            // TODO: Set queue common arguments
            // x-message-ttl = TTL (Time-to-Live) for each message (in ms)
            // x-dead-letter-routing-key = Routing key for DLX messages
            // x-max-priority = Enable message priorities (0–255)
            // x-queue-mode = Set lazy for disk-based queues

            await DeclareQueueForRetries(channel, mainExchangeName, retryExchangeName, queue, cancellationToken);
            
            await DeclareDeadLetterQueueForExhaustedRetries(channel, deadLetterExchangeName, queue, cancellationToken);
        }
    }

    /// <summary>
    /// Delayed retry queue.
    /// This avoids requeuing messages immediately (which can cause tight loops, high CPU usage, and message storming)
    /// and gives you better control over retry intervals.
    /// Flow Overview:
    /// Consumer reads message from the main queue.
    /// - If processing succeeds → Ack.
    /// - If processing fails → Nack (or Reject) with no requeue.
    /// Message is routed to a retry queue (via Dead Letter Exchange).
    /// - Retry queue has a TTL (time-to-live) to delay retries.
    /// - After TTL expires, message is returned to the main queue for reprocessing.
    /// If message keeps failing after max retries → move to a Dead Letter Queue (DLQ).
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="retryExchangeName"></param>
    /// <param name="queueSettings"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="mainExchangeName"></param>
    private static async Task DeclareQueueForRetries(IChannel channel,
        string mainExchangeName,
        string retryExchangeName,
        MessageBrokerQueueSettings queueSettings,
        CancellationToken cancellationToken)
    {
        // Move the message back to the main queue for retries
        var retryArgs = new Dictionary<string, object?>
        {
            { "x-dead-letter-exchange", mainExchangeName },
            { "x-dead-letter-routing-key", queueSettings.BindKey },
            { "x-message-ttl", queueSettings.MessageTimeToLiveInMilliseconds }
            // It defines the maximum time a message can stay in a queue before being discarded or dead-lettered. 
            // It is not an intentional delay mechanism by itself.
            // So, the delay is actually a side effect of message expiration in the retry queue, not a built-in “delay feature.”
        };

        var retryQueueName = $"{queueSettings.Name}.retry";
        await channel.QueueDeclareAsync(
            queue: retryQueueName,
            durable: queueSettings.UsePersistentStorage,
            exclusive: false,
            autoDelete: queueSettings.AutoDelete,
            cancellationToken: cancellationToken,
            arguments: retryArgs);

        await AddBindBetweenExchangeAndQueue(channel, retryExchangeName, retryQueueName, queueSettings.BindKey,
            cancellationToken);
    }

    private static async Task DeclareDeadLetterQueueForExhaustedRetries(IChannel channel,
        string deadLetterExchangeName,
        MessageBrokerQueueSettings queueSettings,
        CancellationToken cancellationToken)
    {
        if (queueSettings.EnableDeadLetter)
        {
            var deadLetterQueueName = $"{queueSettings.Name}.dead";
            await channel.QueueDeclareAsync(
                queue: deadLetterQueueName,
                durable: queueSettings.UsePersistentStorage,
                exclusive: false,
                autoDelete: queueSettings.AutoDelete,
                cancellationToken: cancellationToken,
                arguments: null);

            await AddBindBetweenExchangeAndQueue(channel, deadLetterExchangeName, deadLetterQueueName,
                queueSettings.BindKey, cancellationToken);
        }
    }

    private static async Task AddBindBetweenExchangeAndQueue(IChannel channel,
        string exchangeName,
        string queueName,
        string queueBindKey,
        CancellationToken cancellationToken)
    {
        await channel.QueueBindAsync(
            queue: queueName,
            exchange: exchangeName,
            routingKey: queueBindKey,
            cancellationToken: cancellationToken);
    }
}