using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.Enumerations;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public static class RabbitMqExchangeCreation
{
    public static async Task CreateExchanges(IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await DeclareExchangeAsync(channel, channelSettings.Exchange, cancellationToken);
        await DeclareDeadLetterExchange(channel, channelSettings.Exchange, cancellationToken);
        await DeclareRetryExchange(channel, channelSettings.Exchange, cancellationToken);

        // TODO: Set exchange common arguments
        // alternate-exchange = Fallback exchange if no queue matches
    }
    
    private static async Task DeclareExchangeAsync(IChannel channel,
        MessageBrokerExchangeSettings exchangeSettings,
        CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(
            exchange: exchangeSettings.Name,
            type: exchangeSettings.RoutingType.ToString().ToLowerInvariant(),
            durable: exchangeSettings.UsePersistentStorage,
            autoDelete: exchangeSettings.AutoDelete,
            cancellationToken: cancellationToken);
    }
    
    private static async Task DeclareDeadLetterExchange(IChannel channel,
        MessageBrokerExchangeSettings exchangeSettings,
        CancellationToken cancellationToken)
    {
        var deadLetter = new MessageBrokerExchangeSettings()
        {
            Name = GetDeadLetterExchangeName(exchangeSettings.Name),
            RoutingType = RoutingType.Direct,
            UsePersistentStorage = exchangeSettings.UsePersistentStorage,
            AutoDelete = false
        };

        await DeclareExchangeAsync(channel, deadLetter, cancellationToken);
    }

    private static async Task DeclareRetryExchange(IChannel channel,
        MessageBrokerExchangeSettings exchangeSettings,
        CancellationToken cancellationToken)
    {
        var retryQueue = new MessageBrokerExchangeSettings()
        {
            Name = GetRetryExchangeName(exchangeSettings.Name),
            RoutingType = RoutingType.Direct, // Force Direct to be able to use the same bind key
            UsePersistentStorage = exchangeSettings.UsePersistentStorage,
            AutoDelete = false
        };

        await DeclareExchangeAsync(channel, retryQueue, cancellationToken);
    }
    
    public static string GetRetryExchangeName(string mainExchangeName)
    {
        return $"{mainExchangeName}.retry";
    }
    
    public static string GetDeadLetterExchangeName(string mainExchangeName)
    {
        return $"{mainExchangeName}.dead";
    }
}