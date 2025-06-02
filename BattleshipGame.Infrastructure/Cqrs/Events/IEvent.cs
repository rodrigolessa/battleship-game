namespace BattleshipGame.Infrastructure.Cqrs.Events;

public interface IEvent
{
    // TODO: Find a way to implement the specific type
    string EventKey { get; }
    string IdempotencyKey { get; set; }
    string AggregateId { get; set; }
    string SessionKey { get; set; }
    string ChannelKey { get; set; }
    string ApplicationKey { get; set; }
    string SagaProcessKey { get; set; }
    // TODO: Avoid primitive type implementing an email class
    string UserEmail { get; set; }
    DateTime EventCommittedTimestamp { get; set; }
}