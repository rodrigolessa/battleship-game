using MediatR;

namespace BattleshipGame.WebApi.RequestProcessor;

public interface IRequestProcessor
{
    Task<TResponse> Process<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>;
}