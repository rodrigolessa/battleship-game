using System.Diagnostics.CodeAnalysis;
using frm.Infrastructure.Messaging.Enumerations;

namespace frm.Infrastructure.Messaging.Configurations;

[ExcludeFromCodeCoverage]
public class MessageBrokerSettings
{
    public MessageBrokerSettings()
    {
        Channels = new List<MessageBrokerChannelSettings>();
        NonRetryableExceptionType = new List<string>();
    }

    public static string SectionName => "MessageBrokerSettings";

    public required string HostName { get; set; }
    public required int HostPort { get; set; }
    public required string UserName { get; set; }
    public required string UserPassword { get; set; }
    public bool WaitForMessageToBeSent { get; set; } = false; // DispatchConsumersAsync
    public int MaxLength { get; set; } = 12800000;
    public int MessageMaxLength { get; set; } = 64000;
    public int MessageTimeToLiveInMilliseconds { get; set; } = 60000;
    public MessageType MessageType { get; set; } = MessageType.Json;
    public List<string> NonRetryableExceptionType { get; set; }
    public List<MessageBrokerChannelSettings> Channels { get; set; }
}