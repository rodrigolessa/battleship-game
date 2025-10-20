namespace BattleshipGame.Models.Entities;

public readonly struct PlayerId(string value) : IComparable<PlayerId>, IEquatable<PlayerId>
{
    private string Value { get; } = value;

    public static PlayerId New() => new PlayerId(Ulid.NewUlid().ToString());

    public bool Equals(PlayerId other) => this.Value.Equals(other.Value);
    public int CompareTo(PlayerId other) => String.Compare(Value, other.Value, StringComparison.Ordinal);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is PlayerId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;

    public static bool operator ==(PlayerId a, PlayerId b) => a.CompareTo(b) == 0;
    public static bool operator !=(PlayerId a, PlayerId b) => !(a == b);
}