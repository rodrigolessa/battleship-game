using BattleshipGame.Models.Enumerations;
using Humanizer;

namespace BattleshipGame.Models.Entities;

public class AircraftCarrier : Vessel
{
    public AircraftCarrier()
    {
        Name = OccupationType.AircraftCarrier.Humanize();
        Width = 5;
        OccupationType = OccupationType.AircraftCarrier;
    }
}