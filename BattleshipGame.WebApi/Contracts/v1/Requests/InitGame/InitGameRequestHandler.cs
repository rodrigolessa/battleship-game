using BattleshipGame.Application.Commands.NewGame;
using BattleshipGame.Models;
using BattleshipGame.Models.Entities;
using BattleshipGame.WebApi.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public class InitGameRequestHandler : IInitGameRequestHandler
{
    public Task<ObjectResult> Handle(InitGameRequest request, CancellationToken cancellationToken)
    {
        var gameId = GameId.New();
        var playerOneId = PlayerId.New();
        var playerTwoId = PlayerId.New();
        var command = new NewGameCommand(
            request.IdempotencyKey,
            gameId,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty)
        {
            Player1Id = playerOneId,
            Player1Name = request.Player1,
            Player2Id = playerTwoId,
            Player2Name = request.Player2
        };

        // TODO: Map request to command using Mappely
        // TODO: Schedule command to NATs
        
        var newGameInfo = new NewGameInfoResponse(command.IdempotencyKey, gameId, playerOneId, playerTwoId);

        return Task.FromResult(new ObjectResult(newGameInfo));
    }
}