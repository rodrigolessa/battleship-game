using BattleshipGame.Infrastructure.Abstractions;
using BattleshipGame.Models;
using frm.Infrastructure.EventSourcingCqrs;

namespace BattleshipGame.Events;

public sealed class GameRequested : MyBaseEvent
{
    public GameRequested(GameId id, string eventKey, string idempotencyKey, string sessionKey,
        string applicationKey, string sagaProcessKey, string correlationKey, IClock clock,
        string userEmail = "")
        : base(
            eventKey,
            idempotencyKey,
            id.ToString(),
            sessionKey,
            applicationKey,
            sagaProcessKey,
            correlationKey,
            clock,
            userEmail)
    {
    }
}