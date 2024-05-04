using BattleshipGame.Infrastructure.Cqrs.Commands;
using BattleshipGame.Infrastructure.Cqrs.Queries;
using BattleshipGame.Infrastructure.Exceptions;
using FluentValidation;
using MediatR;

namespace BattleshipGame.Infrastructure.PipelineBehaviors;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        // TODO: Replace WhenAll for another with a best performance
        var validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null);

        if (!failures.Any()) return await next();

        throw request switch
        {
            IBaseCommand => new FrustratedCommandExecutionException(failures),
            IQuery<TResponse> => new FrustratedQueryExecutionException(failures),
            _ => new ValidationException(failures)
        };
    }
}