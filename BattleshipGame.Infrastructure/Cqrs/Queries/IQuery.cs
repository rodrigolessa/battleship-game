using MediatR;

namespace BattleshipGame.Infrastructure.Cqrs.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}