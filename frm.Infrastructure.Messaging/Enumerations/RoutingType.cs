namespace frm.Infrastructure.Messaging.Enumerations;

public enum RoutingType
{
    Unknown,
    Direct,
    Topic,
    Fanout,
    Headers
}