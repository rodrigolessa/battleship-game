namespace BattleshipGame.Infrastructure.ReadModel.Models;

public class GameBoardProjection : Projection
{
    public string PlayerId { get; set; }
    public bool IsAFireBoard { get; set; }
    public List<PanelProjection> Panels { get; set; } = new List<PanelProjection>();
}