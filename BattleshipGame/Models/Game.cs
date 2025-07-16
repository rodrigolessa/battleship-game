using BattleshipGame.Models.Entities;
using frm.Infrastructure.EventSourcing.Models;
using StronglyTypedIds;

namespace BattleshipGame.Models;

[StronglyTypedId]
public partial struct GameId {}

public sealed class Game : AggregateRoot
{
    private const string DefaultNameForPlayer1 = "PlayerOne";
    private const string DefaultNameForPlayer2 = "PlayerTwo";

    public GameId Id { get; private set; }
    public Player Player1 { get; private set; } = new(DefaultNameForPlayer1);
    public Player Player2 { get; private set; } = new(DefaultNameForPlayer2);

    public void PlayRound() { }

    public void PlayToEnd() { }
}