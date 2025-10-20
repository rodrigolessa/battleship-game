using frm.Infrastructure.Cqrs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.CancelGame;

public class CancelGameCommandRequest : ICommandRequest<ObjectResult>
{
    
}