using System.Text;
using System.Text.Json;
using frm.Infrastructure.Cqrs.Commands;
using frm.Infrastructure.Messaging.Abstractions;
using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.RabbitMqSettings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqPublisher;

public class RabbitMqMessagePublisher : ICommandPublisher, IAsyncDisposable
{
    private readonly MessageBrokerSettings _settings;
    private readonly ILogger<RabbitMqMessagePublisher> _logger;
    private readonly IChannel _channel;
    private readonly string _exchangeName;

    public RabbitMqMessagePublisher(MessageBrokerSettings settings,
        IRabbitMqConnectionManager connectionManager,
        ILogger<RabbitMqMessagePublisher> logger)
    {
        _settings = settings;
        _logger = logger;
        _channel = connectionManager.Channel;
        _exchangeName = connectionManager.PrimaryExchangeName;
    }

    public async Task PublishAsync(IBaseCommand command,
        string exchange = "",
        string route = "",
        CancellationToken cancellationToken = default)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(command));
        await _channel.BasicPublishAsync(
            string.IsNullOrWhiteSpace(exchange) ? _exchangeName : exchange,
            routingKey: route,
            mandatory: false,
            body,
            cancellationToken);

        _logger.LogInformation("Message {IdempotencyKey} published to {Route}", command.IdempotencyKey, route);
    }

    public Task PublishAsync<T>(List<T> commands, string exchange, string route = "", int? delayInSeconds = null)
        where T : IBaseCommand
    {
        throw new NotImplementedException();
    }

    // public async Task PublishAsync<T>(T message, string exchange, string route = "", CancellationToken cancellationToken = default)
    // {
    //     var envelope = new MessageEnvelope<T>(message);
    //     var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(envelope));
    //     var props = new BasicProperties();
    //
    //
    //     await _channel.BasicPublishAsync(
    //         exchange: _exchange,
    //         routingKey: route,
    //         mandatory: false,
    //         basicProperties: props,
    //         body: body,
    //         cancellationToken);
    //     
    //     _logger.LogInformation("Published message: {MessageId} to {ChannelKey}", envelope.MessageId, route);
    // }

    // public async Task ScheduleAsync<T>(T message, DateTime scheduledTimeUtc, string exchange, string route = "",
    //     CancellationToken cancellationToken = default)
    // {
    //     var delay = (int)(scheduledTimeUtc - DateTime.UtcNow).TotalMilliseconds;
    //
    //     var envelope = new MessageEnvelope<T>(message);
    //     var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(envelope));
    //
    //     var props = new BasicProperties
    //     {
    //         Expiration = delay.ToString() // TTL in ms
    //     };
    //
    //     // TODO: Understand this concept of a new queue for delayed messages
    //     // Publish to a delay queue (with dead-letter to final queue)
    //     await _channel.BasicPublishAsync(
    //         exchange: "delayed_exchange",
    //         routingKey: route,
    //         mandatory: false,
    //         basicProperties: props,
    //         body: body,
    //         cancellationToken);
    //
    //     _logger.LogInformation("Scheduled message: {MessageId} for {Delay}ms", envelope.MessageId, delay);
    // }

    public async ValueTask DisposeAsync()
    {
        await _channel.DisposeAsync();
    }
}