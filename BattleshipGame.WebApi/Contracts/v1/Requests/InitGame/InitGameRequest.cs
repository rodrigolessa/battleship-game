using frm.Infrastructure.Cqrs.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public class InitGameRequest : MyBaseRequest<ObjectResult>
{
    public required string Player1 { get; set; }
    public required string Player2 { get; set; }
}