using BattleshipGame.Infrastructure.Models;
using BattleshipGame.Models.Entities;
using StronglyTypedIds;

namespace BattleshipGame.Models;

[StronglyTypedId(Template.Guid)]
public partial struct GameId {}

public class Game : AggregateRoot
{
    public const string DefaultNameForPlayer1 = "PlayerOne";
    public const string DefaultNameForPlayer2 = "PlayerTwo";
        
    public GameId Id { get; set; }
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    
    public Game() { }

    public void PlayRound() { }

    public void PlayToEnd() { }
}