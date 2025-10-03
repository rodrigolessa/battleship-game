using frm.Infrastructure.Messaging.Abstractions;
using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.RabbitMqPublisher;
using frm.Infrastructure.Messaging.RabbitMqSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipGame.Infrastructure.IoC.Configurations;

public static class MessageBrokerCollectionExtensions
{
    public static void SetMessageBrokerSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var messageBrokerSettings = configuration
            .GetSection(MessageBrokerSettings.SectionName)
            .Get<MessageBrokerSettings>();
        // TODO: Define a basic default settings
        services.AddSingleton(messageBrokerSettings);
    }

    /// <summary>
    /// RabbitMQ doesn't have the concept of subscriber for topic or queue, the communication is through an exchange
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConnectOrCreateRabbitMqExchangeForPublishMessages(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // RabbitMqConnectionManager, establish connection to create exchange, and it is used inside the publisher class
        services.AddSingleton<IRabbitMqConnectionManager, RabbitMqConnectionManager>();
        services.AddHostedService(provider =>
            (RabbitMqConnectionManager)provider.GetRequiredService<IRabbitMqConnectionManager>());

        services.AddSingleton<ICommandPublisher, RabbitMqMessagePublisher>();
    }

    public static void CreateAllNecessaryRabbitMqInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register background service to create exchange, queues and binds
        services.AddHostedService<RabbitMqInitializer>();
    }
}