using BattleshipGame.Models.Enumerations;
using Humanizer;

namespace BattleshipGame.Models.Entities.Vessels;

public class Destroyer : Vessel
{
    public Destroyer()
    {
        Name = OccupationType.Destroyer.Humanize();
        Width = 2;
        OccupationType = OccupationType.Destroyer;
    }
}