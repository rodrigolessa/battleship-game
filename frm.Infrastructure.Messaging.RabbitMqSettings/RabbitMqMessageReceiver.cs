using System.Text;
using frm.Infrastructure.Messaging.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public class RabbitMqMessageReceiver
{
    private readonly IChannel _channel;
    private readonly string _queueName;
    private readonly string _exchangeName;
    private readonly string _bindKey;
    private readonly int _maxRetries;

    public RabbitMqMessageReceiver(IChannel channel, string exchangeName, string queueName, string bindKey, int maxRetries = 3)
    {
        _channel = channel;
        _exchangeName = exchangeName;
        _queueName = queueName;
        _bindKey = bindKey;
        _maxRetries = maxRetries;
    }

    public async Task StartConsumingAndHandleRetries(Func<string, Task<bool>> handleMessageAsync,
        CancellationToken cancellationToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                await handleMessageAsync(message);
                await _channel.BasicAckAsync(ea.DeliveryTag, false, cancellationToken);
            }
            catch (Exception ex)
            {
                // Add retry header count
                var retryCount = 0;
                var headers = ea.BasicProperties?.Headers ?? new Dictionary<string, object?>();
                if (headers.TryGetValue("x-retry-count", out var retryCountHeaderValue))
                {
                    retryCount = Convert.ToInt32(retryCountHeaderValue);
                }

                retryCount++;

                if (retryCount >= _maxRetries)
                {
                    // Move to DLQ
                    // TODO: Log dead letter decision
                    Console.WriteLine("Max retries reached. Sending to dead-letter queue.");
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false, cancellationToken);
                }
                else
                {
                    // Republish to retry exchange with incremented count
                    var props = new BasicProperties
                    {
                        Headers = headers
                    };
                    props.Headers["x-retry-count"] = retryCount;

                    await _channel.BasicPublishAsync(
                        exchange: RabbitMqExchangeCreation.GetRetryExchangeName(_exchangeName),
                        routingKey: _bindKey,
                        mandatory: false,
                        basicProperties: props,
                        body: ea.Body,
                        cancellationToken);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false, cancellationToken);
                }
            }
        };

        await _channel.BasicConsumeAsync(_queueName, false, consumer, cancellationToken: cancellationToken);
    }
}