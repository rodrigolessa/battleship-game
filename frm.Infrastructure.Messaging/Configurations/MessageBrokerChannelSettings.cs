using frm.Infrastructure.Messaging.Enumerations;

namespace frm.Infrastructure.Messaging.Configurations;

public sealed class MessageBrokerChannelSettings // Exchange, Queue and Binds
{
    // A channel is an Exchange in RabbitMQ
    public bool EnableChannel { get; set; } = false;
    public required string Name { get; set; }
    public ChannelType Type { get; set; } = ChannelType.Direct;
    public bool UsePersistentStorage { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public ushort PrefetchCount { get; set; } = 0;
    public int RetryCount { get; set; } = 3;
    public int RetryIntervalInSeconds { get; set; } = 60;
    public int MessageWaitTimeoutInMilliseconds { get; set; } = 200;
    public bool CanReceiveMessages { get; set; }
    public bool CanPublishMessages { get; set; }
    public bool UseSessions { get; set; }
    // Queue
    public required string QueueName { get; set; }
    // Bind to the exchange
    public string BindKey { get; set; } = "";
}