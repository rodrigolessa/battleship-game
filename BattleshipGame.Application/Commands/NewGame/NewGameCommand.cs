using BattleshipGame.Models;
using BattleshipGame.Models.Entities;
using frm.Infrastructure.Cqrs.Commands;

namespace BattleshipGame.Application.Commands.NewGame;

public sealed class NewGameCommand(
    GameId aggregateId,
    PlayerId player1Id,
    string player1Name,
    PlayerId player2Id,
    string player2Name,
    string? idempotencyKey,
    string? correlationKey,
    string? sagaProcessKey,
    string? applicationKey,
    string? userEmail = null!)
    : Command(aggregateId.ToString(),
        idempotencyKey,
        sessionKey: aggregateId.ToString(),
        correlationKey,
        sagaProcessKey,
        applicationKey,
        userEmail)
{
    public PlayerId Player1Id { get; set; } = player1Id;
    public string Player1Name { get; set; } = player1Name;
    public PlayerId Player2Id { get; set; } = player2Id;
    public string Player2Name { get; set; } = player2Name;
}