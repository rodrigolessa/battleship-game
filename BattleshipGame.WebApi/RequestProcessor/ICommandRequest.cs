using MediatR;

namespace BattleshipGame.WebApi.RequestProcessor;

public interface ICommandRequest<out TResponse> : IRequest<TResponse>
{
}