using frm.Infrastructure.Messaging.Abstractions;
using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.RabbitMqPublisher;
using frm.Infrastructure.Messaging.RabbitMqSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipGame.Infrastructure.IoC.Configurations;

public static class MessageBrokerCollectionExtensions
{
    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerSettings = configuration.GetSection(MessageBrokerSettings.SectionName).Get<MessageBrokerSettings>();
        services.AddSingleton(messageBrokerSettings);
        
        // Bind configuration
        //services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBrokerSettings"));
        
        // Register initializer
        // RabbitMQ doesn't have the concept of subscriber. There is no topic or queue, the communication is through an exchange
        services.AddHostedService<RabbitMqInitializer>();
    }

    public static void ConnectOrCreateRabbitMqExchange(this IServiceCollection services, IConfiguration configuration)
    {
        var messageBrokerSettings = configuration.GetSection(MessageBrokerSettings.SectionName).Get<MessageBrokerSettings>();
        services.AddSingleton(messageBrokerSettings);
        
        services.AddSingleton<IRabbitMqConnectionManager, RabbitMqConnectionManager>();
        services.AddHostedService(provider => (RabbitMqConnectionManager)provider.GetRequiredService<IRabbitMqConnectionManager>());
        services.AddSingleton<ICommandPublisher, RabbitMqMessagePublisher>();
    }
}