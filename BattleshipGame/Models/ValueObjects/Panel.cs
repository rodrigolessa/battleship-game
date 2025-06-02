using System.ComponentModel;
using BattleshipGame.Infrastructure.Models;
using BattleshipGame.Models.Enumerations;
using BattleshipGame.Models.ValueObjects;
using Humanizer;

namespace BattleshipGame.Models.Entities;

/// <summary>
/// 
/// </summary>
public class Panel
{
    public OccupationType OccupationType { get; set; }
    public Coordinates Coordinates { get; set; }

    public Panel(int row, int column)
    {
        Coordinates = new Coordinates(row, column);
        OccupationType = OccupationType.Empty;
    }

    public string Status => OccupationType.Humanize();

    public bool IsOccupied =>
        OccupationType is OccupationType.Battleship
            or OccupationType.Destroyer
            or OccupationType.Cruiser
            or OccupationType.Submarine
            or OccupationType.AircraftCarrier;

    public bool IsRandomAvailable =>
        (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
        || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
}