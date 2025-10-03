namespace BattleshipGame.Infrastructure.ReadModel;

public abstract class Projection
{
    public string Id { get; set; }
    public int AggregateVersion { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
}