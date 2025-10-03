namespace frm.Infrastructure.Messaging.Configurations;

public class MessageBrokerQueueSettings
{
    public string Name { get; set; }
    public string BindKey { get; set; }
    public bool UsePersistentStorage { get; set; } = false;
    public bool AutoDelete { get; set; } = false;
    public int RetryCount { get; set; } = 3;
    public int RetryIntervalInSeconds { get; set; } = 60;
    public int MessageWaitTimeoutInMilliseconds { get; set; } = 200;
    public int MessageTimeToLiveInMilliseconds { get; set; } = 60000;
    public bool CanReceiveMessages { get; set; } =  true;
    public bool CanPublishMessages { get; set; } =  true;
    public bool UseSessions { get; set; } =  true;

    public bool EnableDeadLetter { get; set; } = true;
}