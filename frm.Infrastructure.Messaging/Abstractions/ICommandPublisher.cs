using frm.Infrastructure.Cqrs.Commands;

namespace frm.Infrastructure.Messaging.Abstractions;

public interface ICommandPublisher
{
    Task PublishAsync(IBaseCommand command,
        string exchange = "",
        string route = "",
        CancellationToken cancellationToken = default);
    Task PublishAsync<T>(List<T> commands,
        string exchange = "",
        string route = "",
        int? delayInSeconds = null)
        where T : IBaseCommand;
    // Task PublishAsync<T>(T message, string exchange, string route = "", CancellationToken cancellationToken = default);
    // Send later (delayed messages using TTL + dead-letter exchange pattern)
    // Task ScheduleAsync<T>(T message, DateTime scheduledTimeUtc, string exchange = "", string route = "", CancellationToken cancellationToken = default);
}