using BattleshipGame.Infrastructure.Cqrs.Events;

namespace BattleshipGame.Infrastructure.Brokers.Abstractions;

public interface IEventPublisher
{
    Task PublishAsync(IEvent @event, int? delayInSeconds = null);
    Task PublishAsync(IList<IEvent> events, int? delayInSeconds = null);
}