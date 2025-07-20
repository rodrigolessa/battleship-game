using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.RabbitMqSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipGame.Application.Configurations;

public static class MessageBrokerCollectionExtensions
{
    public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind configuration
        services.Configure<MessageBrokerSettings>(
        configuration.GetSection("MessageBrokerSettings"));
        
        // Register initializer
        services.AddHostedService<RabbitMqInitializer>();
    }
}