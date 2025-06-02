using System.Net.Mime;
using BattleshipGame.WebApi.Contracts.v1.Requests.CancelGame;
using BattleshipGame.WebApi.Contracts.v1.Requests.Fire;
using BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;
using BattleshipGame.WebApi.RequestProcessor;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{v:apiVersion}/game")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IRequestProcessor _processor;

    public GameController(ILogger<GameController> logger, IRequestProcessor processor)
    {
        _logger = logger;
        _processor = processor;
    }

    [HttpPost("init")]
    [Consumes("application/json")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [Tags(nameof(InitGame))]
    public async Task<IActionResult> InitGame(InitGameRequest request) =>
        await _processor.Process<InitGameRequest, ObjectResult>(request);

    [HttpPut("{gameId}/fire")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [Tags(nameof(Fire))]
    public async Task<IActionResult> Fire(FireRequest request) =>
        await _processor.Process<FireRequest, ObjectResult>(request);

    [HttpDelete("{gameId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    [Tags(nameof(SeekAndDestroy))]
    public async Task<IActionResult> SeekAndDestroy(CancelGameCommandRequest commandRequest) =>
        await _processor.Process<CancelGameCommandRequest, ObjectResult>(commandRequest);
}