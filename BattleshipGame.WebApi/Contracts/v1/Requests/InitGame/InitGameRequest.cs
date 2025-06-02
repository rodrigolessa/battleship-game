using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public class InitGameRequest : IRequest<ObjectResult>
{
    public string IdempotencyKey { get; set; }
    public string Player1 { get; set; }
    public string Player2 { get; set; }
}