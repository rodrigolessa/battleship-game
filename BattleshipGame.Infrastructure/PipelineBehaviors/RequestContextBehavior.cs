using BattleshipGame.Infrastructure.RequestsContext;
using frm.Infrastructure.Cqrs.Requests;
using MediatR;

namespace BattleshipGame.Infrastructure.PipelineBehaviors;

/// <summary>
/// Pipeline Behavior to Enrich Commands
/// </summary>
public class RequestContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    private readonly IRequestContextBundle _contextBundle;

    public RequestContextBehavior(IRequestContextBundle contextBundle)
    {
        _contextBundle = contextBundle;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is MyBaseRequest<TResponse> baseCommand)
        {
            baseCommand.ClientApplication = _contextBundle.ClientApplication;
            baseCommand.IpAddress = _contextBundle.IpAddress;
            baseCommand.UserEmail = _contextBundle.UserEmail;

            baseCommand.IdempotencyKey = _contextBundle.IdempotencyKey;
            baseCommand.CorrelationKey = _contextBundle.CorrelationKey;
            baseCommand.SagaProcessKey = _contextBundle.SagaProcessKey;
        }

        return await next();
    }
}