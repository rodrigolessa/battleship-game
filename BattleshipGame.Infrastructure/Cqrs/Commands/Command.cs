using MediatR;

namespace BattleshipGame.Infrastructure.Cqrs.Commands;

public abstract class Command : BaseCommand, IRequest
{
    protected Command(
        string idempotencyKey,
        string aggregateId,
        string sessionKey,
        string channelKey,
        string applicationKey,
        string sagaProcessKey,
        string userEmail = null)
        : base(idempotencyKey, aggregateId, sessionKey, channelKey, applicationKey, sagaProcessKey, userEmail)
    {
    }
}