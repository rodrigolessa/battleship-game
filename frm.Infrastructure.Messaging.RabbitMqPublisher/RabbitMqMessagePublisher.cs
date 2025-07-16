using System.Text;
using System.Text.Json;
using frm.Infrastructure.Cqrs.Commands;
using frm.Infrastructure.Messaging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqPublisher;

public class RabbitMqMessagePublisher : ICommandPublisher, IAsyncDisposable
{
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMqMessagePublisher> _logger;
    private IConnection _connection;
    private IChannel _channel;
    
    public RabbitMqMessagePublisher(IOptions<RabbitMQOptions> options, ILogger<RabbitMqMessagePublisher> logger)
    {
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = options.Value.HostName,
            Port = options.Value.Port,
            UserName = options.Value.UserName,
            Password = options.Value.Password
        };
    }
    
    public async Task InitializeAsync()
    {
        _connection = await _factory.CreateConnectionAsync();
        // TODO: Reuse IModel when possible — don’t open/close channels per message.
        _channel = await _connection.CreateChannelAsync();
    }
    
    public Task PublishAsync(IBaseCommand command, string channelKey = "", int? delayInSeconds = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task PublishAsync<T>(List<T> commands, string channelKey = "", int? delayInSeconds = null) where T : IBaseCommand
    {
        throw new NotImplementedException();
    }

    public async Task PublishAsync<T>(T message, string channelKey = "", CancellationToken cancellationToken = default)
    {
        var envelope = new MessageEnvelope<T>(message);
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(envelope));
        var props = new BasicProperties();


        await _channel.BasicPublishAsync(
            exchange: "my_exchange",
            routingKey: channelKey,
            mandatory: false,
            basicProperties: props,
            body: body,
            cancellationToken);
        
        _logger.LogInformation("Published message: {MessageId} to {ChannelKey}", envelope.MessageId, channelKey);
    }

    public async Task ScheduleAsync<T>(T message, DateTime scheduledTimeUtc, string channelKey = "",
        CancellationToken cancellationToken = default)
    {
        var delay = (int)(scheduledTimeUtc - DateTime.UtcNow).TotalMilliseconds;

        var envelope = new MessageEnvelope<T>(message);
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(envelope));

        var props = new BasicProperties
        {
            Expiration = delay.ToString() // TTL in ms
        };

        // Publish to a delay queue (with dead-letter to final queue)
        await _channel.BasicPublishAsync(
            exchange: "delayed_exchange",
            routingKey: channelKey,
            mandatory: false,
            basicProperties: props,
            body: body,
            cancellationToken);

        _logger.LogInformation("Scheduled message: {MessageId} for {Delay}ms", envelope.MessageId, delay);
    }

    public async ValueTask DisposeAsync()
    {
        _channel?.Dispose();
        await _connection.DisposeAsync();
    }
}