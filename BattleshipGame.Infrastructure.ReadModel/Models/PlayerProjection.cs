namespace BattleshipGame.Infrastructure.ReadModel.Models;

public class PlayerProjection : Projection
{
    public string GameId { get; set; }
    public string Name { get; set; }
    public GameBoardProjection GameBoard { get; set; }
    public GameBoardProjection FiringBoard { get; set; }
    public List<VesselProjection> Ships { get; set; } = new List<VesselProjection>();
    public bool HasLost { get; set; }
}