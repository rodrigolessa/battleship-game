using BattleshipGame.Models.Enumerations;
using Humanizer;

namespace BattleshipGame.Models.Entities;

public class Submarine : Vessel
{
    public Submarine()
    {
        Name = OccupationType.Submarine.Humanize();
        Width = 3;
        OccupationType = OccupationType.Submarine;
    }
}