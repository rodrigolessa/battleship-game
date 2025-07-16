namespace frm.Infrastructure.Cqrs.Commands;

public abstract class BaseCommand : IBaseCommand
{
    public string IdempotencyKey { get; set; }
    public string AggregateId { get; set; }
    public string SessionKey { get; set; }
    public string ChannelKey { get; set; }
    public string ApplicationKey { get; set; }
    public string SagaProcessKey { get; set; }
    public string UserEmail { get; set; }
    public DateTime Timestamp { get; set; }

    protected BaseCommand(
        string idempotencyKey,
        string aggregateId,
        string sessionKey,
        string channelKey,
        string applicationKey,
        string sagaProcessKey,
        string userEmail = "")
    {
        IdempotencyKey = idempotencyKey;
        AggregateId = aggregateId;
        SessionKey = sessionKey;
        ChannelKey = channelKey;
        ApplicationKey = applicationKey;
        SagaProcessKey = sagaProcessKey;
        UserEmail = userEmail;
        Timestamp = DateTime.UtcNow;
        
        SetIdempotencyKey();
    }

    private void SetIdempotencyKey()
    {
        if (string.IsNullOrWhiteSpace(IdempotencyKey))
        {
            IdempotencyKey = BaseCommandIdempotencyKey.New().ToString();
        }
    }
}