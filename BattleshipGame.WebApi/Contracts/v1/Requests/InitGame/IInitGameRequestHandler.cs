using BattleshipGame.WebApi.Contracts.v1.Responses;
using MediatR;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public interface IInitGameRequestHandler : IRequestHandler<InitGameRequest, NewGameInfoResponse>
{
    
}