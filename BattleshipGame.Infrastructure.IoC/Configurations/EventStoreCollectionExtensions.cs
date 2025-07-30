using Microsoft.Extensions.Configuration;

namespace BattleshipGame.Infrastructure.IoC.Configurations;

public static class EventStoreCollectionExtensions
{
    public static void AddEventStore(this IServiceProvider services, IConfiguration configuration)
    {
        // Add EF Context
    }
}