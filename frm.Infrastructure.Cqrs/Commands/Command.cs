using MediatR;

namespace frm.Infrastructure.Cqrs.Commands;

public abstract class Command(
    string idempotencyKey,
    string aggregateId,
    string sessionKey,
    string applicationKey,
    string sagaProcessKey,
    string userEmail = null!)
    : MyBaseCommand(idempotencyKey, aggregateId, sessionKey, applicationKey, sagaProcessKey, userEmail),
        IRequest;