using System.ComponentModel;

namespace BattleshipGame.Models.Enumerations;

public enum OccupationType
{
    [Description("O")]
    Empty = 0,

    [Description("B")]
    Battleship = 1,

    [Description("C")]
    Cruiser = 2,

    [Description("D")]
    Destroyer = 3,

    [Description("S")]
    Submarine = 4,

    [Description("A")]
    AircraftCarrier = 5,

    [Description("X")]
    Hit = 6,

    [Description("M")]
    Miss = 7   
}