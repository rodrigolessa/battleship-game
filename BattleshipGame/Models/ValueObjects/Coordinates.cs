namespace BattleshipGame.Models.ValueObjects;

/// <summary>
/// Represents a location on a board that can be fired at and, the coordinates that are under attack
/// </summary>
/// <param name="Row"></param>
/// <param name="Column"></param>
public record struct Coordinates(short Row, short Column);