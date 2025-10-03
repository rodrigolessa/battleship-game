using BattleshipGame.Models.Entities.Boards;
using BattleshipGame.Models.Entities.Vessels;
using frm.Infrastructure.EventSourcing.Models;
using StronglyTypedIds;

namespace BattleshipGame.Models.Entities;

[StronglyTypedId]
public partial struct PlayerId {}

public class Player : Entity
{
    public const int MaxLengthOfPlayerName = 50;
    
    public PlayerId Id { get; set; }
    public string Name { get; set; }
    public GameBoard GameBoard { get; set; }
    public FiringBoard FiringBoard { get; set; }
    public List<Vessel> Ships { get; set; }
    public bool HasLost
    {
        get
        {
            return Ships.All(x => x.IsSunk);
        }
    }

    public Player(string name)
    {
        Name = name;
        Ships =
        [
            new Destroyer(),
            new Submarine(),
            new Cruiser(),
            new Battleship(),
            new AircraftCarrier()
        ];
        GameBoard = new GameBoard();
        FiringBoard = new FiringBoard();
    }
}