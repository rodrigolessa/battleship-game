using BattleshipGame.Infrastructure.Exceptions;
using frm.Infrastructure.Messaging.Configurations;
using frm.Infrastructure.Messaging.Enumerations;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace frm.Infrastructure.Messaging.RabbitMqSettings;

public class RabbitMqInitializer(MessageBrokerSettings settings) : IHostedService
{
    private IConnection? _connection;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(settings.HostName) || settings.HostPort == 0 ||
            string.IsNullOrWhiteSpace(settings.UserName) || string.IsNullOrWhiteSpace(settings.UserPassword))
        {
            throw new ConfigurationErrorException("The message broker setting is missing or empty.");
        }

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

        _connection = await factory.CreateConnectionAsync(cancellationToken);

        await CreateAllChannels(cancellationToken);
    }

    private async Task CreateAllChannels(CancellationToken cancellationToken)
    {
        foreach (var channelSettings in settings.Channels)
        {
            if (_connection is not { IsOpen: true }) continue;

            if (!channelSettings.EnableChannel) continue;

            var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await SetUnackedMessageLimit(channel, channelSettings, cancellationToken);

            await RabbitMqExchangeCreation.CreateExchanges(channel, channelSettings, cancellationToken);

            await RabbitMqQueueCreation.CreateAnyNecessaryQueues(channel, channelSettings, cancellationToken);
        }
    }

    /// <summary>
    /// In RabbitMQ, prefetch size (set via BasicQos) refers to the number of bytes of unacknowledged messages
    /// allowed per consumer before RabbitMQ stops delivering more.
    /// How to Estimate a Good prefetchCount
    /// Instead of prefetchSize, calculate a good prefetch count based on:
    ///     prefetchCount = processingTimeInMs * throughputPerSecond
    /// Example:
    ///     Avg processing time: 500ms
    ///     Desired throughput: 10 msg/sec
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="channelSettings"></param>
    /// <param name="cancellationToken"></param>
    private static async Task SetUnackedMessageLimit(
        IChannel channel,
        MessageBrokerChannelSettings channelSettings,
        CancellationToken cancellationToken)
    {
        await channel.BasicQosAsync(0, channelSettings.PrefetchCount, false, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_connection is not null)
        {
            await _connection.CloseAsync(cancellationToken: cancellationToken);
        }
    }
}