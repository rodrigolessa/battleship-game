using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public class InitGameRequest(string idempotencyKey) : IRequest<ObjectResult>
{
    public string IdempotencyKey { get; set; } = idempotencyKey;
    public required string Player1 { get; set; }
    public required string Player2 { get; set; }
}