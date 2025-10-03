using MediatR;

namespace frm.Infrastructure.Cqrs.Commands;

public abstract class Command(
    string aggregateId,
    string? idempotencyKey,
    string sessionKey,
    string? correlationKey,
    string? sagaProcessKey,
    string? applicationKey,
    string? userEmail = null!)
    : MyBaseCommand(aggregateId, idempotencyKey, sessionKey, correlationKey, sagaProcessKey, applicationKey, userEmail),
        IRequest;