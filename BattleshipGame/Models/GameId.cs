namespace BattleshipGame.Models;

public readonly struct GameId(string value) : IComparable<GameId>, IEquatable<GameId>
{
    private string Value { get; } = value;

    public static GameId New() => new GameId(Ulid.NewUlid().ToString());

    public bool Equals(GameId other) => this.Value.Equals(other.Value);
    public int CompareTo(GameId other) => String.Compare(Value, other.Value, StringComparison.Ordinal);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is GameId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(GameId a, GameId b) => a.CompareTo(b) == 0;
    public static bool operator !=(GameId a, GameId b) => !(a == b);
}