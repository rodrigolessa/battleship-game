using MediatR;

namespace BattleshipGame.Infrastructure.Cqrs.Commands;

public abstract class Command(
    string idempotencyKey,
    string aggregateId,
    string sessionKey,
    string channelKey,
    string applicationKey,
    string sagaProcessKey,
    string userEmail = null!)
    : BaseCommand(idempotencyKey, aggregateId, sessionKey, channelKey, applicationKey, sagaProcessKey, userEmail),
        IRequest;