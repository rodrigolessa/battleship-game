using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public interface IInitGameRequestHandler : IRequestHandler<InitGameRequest, ObjectResult>
{
    
}