using BattleshipGame.Models.Enumerations;
using BattleshipGame.Models.ValueObjects;

namespace BattleshipGame.Infrastructure.ReadModel.Models;

public class PanelProjection : Projection
{
    public string GameBoardId { get; set; }
    public OccupationType OccupationType { get; set; }
    public short Row { get; set; }
    public short Column { get; set; }
}