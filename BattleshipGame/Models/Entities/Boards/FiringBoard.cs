using BattleshipGame.Models.ValueObjects;

namespace BattleshipGame.Models.Entities.Boards;

public class FiringBoard : GameBoard
{
    public List<Coordinates> GetOpenRandomPanels()
    {
        return new List<Coordinates>();
    }

    public List<Coordinates> GetHitNeighbors()
    {
        return new List<Coordinates>();
    }

    public List<Panel> GetNeighbors(Coordinates coordinates)
    {
        return new List<Panel>();
    }
}