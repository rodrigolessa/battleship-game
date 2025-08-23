using frm.Infrastructure.Messaging.Enumerations;

namespace frm.Infrastructure.Messaging.Configurations;

public class MessageBrokerExchangeSettings
{
    public string Name { get; set; }
    public RoutingType RoutingType { get; set; } = RoutingType.Direct;
    public bool UsePersistentStorage { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
}