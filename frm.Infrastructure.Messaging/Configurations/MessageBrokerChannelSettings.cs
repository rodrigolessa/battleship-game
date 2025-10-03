using frm.Infrastructure.Messaging.Enumerations;

namespace frm.Infrastructure.Messaging.Configurations;

public sealed class MessageBrokerChannelSettings // Declaration of Exchange, Queues and Binds
{
    // * A channel is a virtual connection inside a physical TCP connection.
    // * Why channels exist?
    // - Opening and maintaining a TCP connection is relatively expensive.
    // - Many applications need multiple "logical connections" (for publishing, consuming, admin tasks, etc.).
    // - Instead of opening many TCP connections, RabbitMQ allows you to open many channels over a single connection.
    public MessageBrokerChannelSettings()
    {
        Exchange = new MessageBrokerExchangeSettings();
        Queues = new List<MessageBrokerQueueSettings>();
    }
    
    public bool EnableChannel { get; set; } = false;
    public bool UsePersistentStorage { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public ushort PrefetchCount { get; set; } = 0;
    public MessageBrokerExchangeSettings Exchange { get; set; }
    public List<MessageBrokerQueueSettings> Queues { get; set; }
}