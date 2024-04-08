using BattleshipGame.Models.Enumerations;
using Humanizer;

namespace BattleshipGame.Models.Entities;

public class Cruiser : Vessel
{
    public Cruiser()
    {
        Name = OccupationType.Cruiser.Humanize();
        Width = 3;
        OccupationType = OccupationType.Cruiser;
    }
}