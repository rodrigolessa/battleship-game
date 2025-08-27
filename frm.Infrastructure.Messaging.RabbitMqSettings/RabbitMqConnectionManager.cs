using BattleshipGame.Infrastructure.Exceptions;
using frm.Infrastructure.Messaging.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public sealed class RabbitMqConnectionManager : BackgroundService, IRabbitMqConnectionManager
{
    private readonly MessageBrokerChannelSettings _channelSettings;
    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IChannel _channel;
    private string _exchange;

    public IConnection Connection => _connection?? throw new InvalidOperationException("RabbitMQ connection is not initialized.");
    public IChannel Channel => _channel?? throw new InvalidOperationException("RabbitMQ channel is not initialized.");
    public string PrimaryExchangeName => _exchange;
    

    public RabbitMqConnectionManager(MessageBrokerSettings settings)
    {
        // TODO: Remove duplicate code
        if (string.IsNullOrWhiteSpace(settings.HostName) || settings.HostPort == 0 ||
            string.IsNullOrWhiteSpace(settings.UserName) || string.IsNullOrWhiteSpace(settings.UserPassword))
        {
            throw new ConfigurationErrorException("The message broker setting is missing or empty.");
        }

        _connectionFactory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.HostPort,
            UserName = settings.UserName,
            Password = settings.UserPassword,
            VirtualHost = "/",
            ConsumerDispatchConcurrency = 1
            // A value greater than one enables parallelism for a single consumer on a single session/channel, within the limits of the prefetchCount
        };
        
        _channelSettings = settings.Channels.First(x => x.EnableChannel);
        _exchange = _channelSettings.Exchange.Name;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connection = await _connectionFactory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
        
        // Make sure the exchange exists
        await RabbitMqInitializer.CreateTheExchange(_channel, _channelSettings, stoppingToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _channel.DisposeAsync();
    }
}