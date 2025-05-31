namespace BattleshipGame.Models;

public readonly struct OldGameId : IComparable<OldGameId>, IEquatable<OldGameId>
{
    public Guid Value { get; }

    public OldGameId(Guid value)
    {
        Value = value;
    }

    public static OldGameId New() => new OldGameId(Guid.NewGuid());

    public bool Equals(OldGameId other) => this.Value.Equals(other.Value);
    public int CompareTo(OldGameId other) => Value.CompareTo(other.Value);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is OldGameId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();

    public static bool operator ==(OldGameId a, OldGameId b) => a.CompareTo(b) == 0;
    public static bool operator !=(OldGameId a, OldGameId b) => !(a == b);
}