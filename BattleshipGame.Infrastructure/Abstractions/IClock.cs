namespace BattleshipGame.Infrastructure.Abstractions;

public interface IClock
{
    DateTime UtcNow();
}