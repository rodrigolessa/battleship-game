using BattleshipGame.Application.Commands.NewGame;
using BattleshipGame.Application.Configurations;
using BattleshipGame.Models;
using BattleshipGame.Models.Entities;
using BattleshipGame.WebApi.Contracts.v1.Responses;
using frm.Infrastructure.Messaging.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipGame.WebApi.Contracts.v1.Requests.InitGame;

public class InitGameRequestHandler : IInitGameRequestHandler
{
    private ICommandPublisher _commandPublisher;

    public InitGameRequestHandler(ICommandPublisher commandPublisher)
    {
        _commandPublisher = commandPublisher;
    }

    public async Task<ObjectResult> Handle(InitGameRequest request, CancellationToken cancellationToken)
    {
        var gameId = GameId.New();
        var playerOneId = PlayerId.New();
        var playerTwoId = PlayerId.New();
        var command = new NewGameCommand(
            gameId,
            playerOneId,
            request.Player1,
            playerTwoId,
            request.Player2,
            request.IdempotencyKey,
            request.CorrelationKey,
            request.SagaProcessKey,
            request.ClientApplication,
            request.UserEmail
            ) { };

        // TODO: Move the responsibility for choosing the channel to the messaging package
        // TODO: Add a property in the MessageBrokerSettings to map commands to their respective binds
        await _commandPublisher.PublishAsync(command, "", MessageBrokerConstants.NewGameRoute, cancellationToken);

        var newGameInfo = new NewGameInfoResponse(command.IdempotencyKey, gameId, playerOneId, playerTwoId);

        return new ObjectResult(newGameInfo);
    }
}