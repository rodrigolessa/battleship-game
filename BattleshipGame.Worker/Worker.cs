using BattleshipGame.Application.CommandHandlers;
using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.RabbitMqSettings;
using RabbitMQ.Client;

namespace BattleshipGame.Worker;

public class Worker(ILogger<Worker> logger, MessageBrokerSettings settings) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.HostPort,
            UserName = settings.UserName,
            Password = settings.UserPassword,
            VirtualHost = "/",
            ConsumerDispatchConcurrency = 1
            // A value greater than one enables parallelism for a single consumer on a single session/channel, within the limits of the prefetchCount
        };

        var connection = await factory.CreateConnectionAsync(stoppingToken);
        
        foreach (var channelSettings in settings.Channels)
        {
            if (connection is not { IsOpen: true }) continue;

            if (!channelSettings.EnableChannel) continue;

            var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await RabbitMqInitializer.SetUnackedMessageLimit(channel, channelSettings, stoppingToken);

            foreach (var queue in channelSettings.Queues)
            {
                var exchangeName = channelSettings.Exchange.Name;
                var queueName = queue.Name;
                var bindKey = queue.BindKey;
                var receiver = new RabbitMqMessageReceiver(channel, exchangeName, queueName, bindKey);
                //var handler = new NewGameCommandHandler();
                //receiver.StartConsumingAndHandleRetries(handler);
            }
        }
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     if (logger.IsEnabled(LogLevel.Information))
        //     {
        //         logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     }
        //     await Task.Delay(1000, stoppingToken);
        // }
    }
}