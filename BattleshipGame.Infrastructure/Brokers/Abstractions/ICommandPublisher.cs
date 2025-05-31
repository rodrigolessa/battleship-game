using BattleshipGame.Infrastructure.Cqrs.Commands;

namespace BattleshipGame.Infrastructure.Brokers.Abstractions;

public interface ICommandPublisher
{
    Task PublishAsync(IBaseCommand command, int? delayInSeconds = null);
    Task PublishAsync<T>(List<T> commands, int? delayInSeconds = null) where T : IBaseCommand;
}