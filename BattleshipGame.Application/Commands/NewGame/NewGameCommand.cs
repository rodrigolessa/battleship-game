using BattleshipGame.Infrastructure.Cqrs.Commands;
using BattleshipGame.Models;
using BattleshipGame.Models.Entities;

namespace BattleshipGame.Application.Commands.NewGame;

public class NewGameCommand : Command
{
    public PlayerId Player1Id { get; set; }
    public string Player1Name { get; set; }
    public PlayerId Player2Id { get; set; }
    public string Player2Name { get; set; }

    public NewGameCommand(
        string idempotencyKey,
        GameId aggregateId,
        string sessionKey,
        string channelKey,
        string applicationKey,
        string sagaProcessKey,
        string userEmail = null)
        : base(idempotencyKey, aggregateId.ToString(), sessionKey, channelKey, applicationKey, sagaProcessKey, userEmail)
    {
    }
}