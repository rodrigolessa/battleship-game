using BattleshipGame.Models.Enumerations;

namespace BattleshipGame.Models.Entities;

public abstract class Vessel
{
    public string Name { get; set; }
    public int Width { get; set; }
    public int Hits { get; set; }
    public OccupationType OccupationType { get; set; }
    public bool IsSunk => Hits >= Width;
}