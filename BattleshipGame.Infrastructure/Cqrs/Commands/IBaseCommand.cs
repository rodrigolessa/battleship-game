using StronglyTypedIds;

namespace BattleshipGame.Infrastructure.Cqrs.Commands;

[StronglyTypedId(Template.Guid)]
public partial struct BaseCommandIdempotencyKey { }

public interface IBaseCommand
{
    // TODO: Find an way to implement the specific type, BaseCommandIdempotencyKey
    string IdempotencyKey { get; set; }
    string AggregateId { get; set; }
    string SessionKey { get; set; }
    string ChannelKey { get; set; }
    string ApplicationKey { get; set; }
    string SagaProcessKey { get; set; }
    // TODO: Avoid primitive type implementing an email class
    string UserEmail { get; set; }
    DateTime Timestamp { get; set; }
}