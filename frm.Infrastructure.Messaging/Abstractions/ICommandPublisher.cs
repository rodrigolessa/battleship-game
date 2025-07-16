using frm.Infrastructure.Cqrs.Commands;

namespace frm.Infrastructure.Messaging.Abstractions;

public interface ICommandPublisher
{
    Task PublishAsync(IBaseCommand command, string channelKey = "", int? delayInSeconds = null, CancellationToken cancellationToken = default);
    Task PublishAsync<T>(List<T> commands, string channelKey = "", int? delayInSeconds = null) where T : IBaseCommand;
    Task PublishAsync<T>(T message, string channelKey = "", CancellationToken cancellationToken = default);
    // Send later (delayed messages using TTL + dead-letter exchange pattern)
    Task ScheduleAsync<T>(T message, DateTime scheduledTimeUtc, string channelKey = "", CancellationToken cancellationToken = default);
}