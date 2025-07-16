namespace frm.Infrastructure.Cqrs.Commands;

public interface IBaseCommand
{
    // TODO: Find a way to implement the specific type, BaseCommandIdempotencyKey
    string IdempotencyKey { get; set; }
    string AggregateId { get; set; }
    string SessionKey { get; set; }
    // TODO: Move responsibility to the messaging package
    string ChannelKey { get; set; }
    string ApplicationKey { get; set; }
    string SagaProcessKey { get; set; }
    // TODO: Avoid primitive type implementing an email class
    string UserEmail { get; set; }
    DateTime Timestamp { get; set; }
}