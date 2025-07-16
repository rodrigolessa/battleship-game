using frm.Infrastructure.EventSourcing.Events;

namespace frm.Infrastructure.Messaging.Abstractions;

public interface IEventPublisher
{
    Task PublishAsync(IEvent @event, int? delayInSeconds = null);
    Task PublishAsync(IList<IEvent> events, int? delayInSeconds = null);
}