using BattleshipGame.Infrastructure.Abstractions;

namespace BattleshipGame.Infrastructure;

public sealed class MyClock: IClock
{
    public DateTime UtcNow() => DateTime.UtcNow;
}