using BattleshipGame.Models.Enumerations;

namespace BattleshipGame.Infrastructure.ReadModel.Models;

public class VesselProjection : Projection
{
    public string PlayerId { get; set; }
    public string Name { get; set; }
    public int Width { get; set; }
    public int Hits { get; set; }
    public OccupationType OccupationType { get; set; }
    public bool IsSunk { get; set; }
}