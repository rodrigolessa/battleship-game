using BattleshipGame.Models.Enumerations;

namespace BattleshipGame.Infrastructure.ReadModel.Models;

public class GameProjection : Projection
{
    public PlayerProjection Player1 { get; set; }
    public PlayerProjection Player2 { get; set; }
    public GameStatus Status { get; set; }
}