namespace BattleshipGame.Models.Entities.Boards;

public readonly struct GameBoardId(string value) : IComparable<GameBoardId>, IEquatable<GameBoardId>
{
    private string Value { get; } = value;

    public static GameBoardId New() => new GameBoardId(Ulid.NewUlid().ToString());

    public bool Equals(GameBoardId other) => this.Value.Equals(other.Value);
    public int CompareTo(GameBoardId other) => String.Compare(Value, other.Value, StringComparison.Ordinal);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is GameBoardId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(GameBoardId a, GameBoardId b) => a.CompareTo(b) == 0;
    public static bool operator !=(GameBoardId a, GameBoardId b) => !(a == b);
}