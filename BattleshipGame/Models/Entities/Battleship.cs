using BattleshipGame.Models.Enumerations;
using Humanizer;

namespace BattleshipGame.Models.Entities;

public class Battleship : Vessel
{
    public Battleship()
    {
        Name = OccupationType.Battleship.Humanize();
        Width = 4;
        OccupationType = OccupationType.Battleship;
    }
}