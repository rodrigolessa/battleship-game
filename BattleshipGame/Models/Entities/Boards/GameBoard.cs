using frm.Infrastructure.EventSourcing.Models;
using StronglyTypedIds;

namespace BattleshipGame.Models.Entities.Boards;

[StronglyTypedId(Template.Guid)]
public partial struct GameBoardId {}

public class GameBoard : Entity
{
    public GameBoardId Id { get; set; }
    public List<Panel> Panels { get; set; }

    public GameBoard()
    {
        Panels = new List<Panel>();
        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                Panels.Add(new Panel(i, j));
            }
        }
    }
}