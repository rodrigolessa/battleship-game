using BattleshipGame.Infrastructure.Cqrs.Commands;
using BattleshipGame.Models;
using BattleshipGame.Models.Entities;

namespace BattleshipGame.Application.Commands.NewGame;

public sealed class NewGameCommand(
    string idempotencyKey,
    GameId aggregateId,
    string sessionKey,
    string channelKey,
    string applicationKey,
    string sagaProcessKey,
    PlayerId player1Id,
    string player1Name,
    PlayerId player2Id,
    string player2Name,
    string userEmail = null!)
    : Command(idempotencyKey, aggregateId.ToString(), sessionKey, channelKey, applicationKey, sagaProcessKey,
        userEmail)
{
    public PlayerId Player1Id { get; set; } = player1Id;
    public string Player1Name { get; set; } = player1Name;
    public PlayerId Player2Id { get; set; } = player2Id;
    public string Player2Name { get; set; } = player2Name;
}