namespace frm.Infrastructure.Cqrs.Commands;

public abstract class MyBaseCommand : IBaseCommand
{
    public string IdempotencyKey { get; set; }
    public string AggregateId { get; set; }
    public string SessionKey { get; set; }
    public string ApplicationKey { get; set; }
    public string SagaProcessKey { get; set; }
    public string CorrelationKey { get; set; }
    public string UserEmail { get; set; }
    public DateTime Timestamp { get; set; }

    protected MyBaseCommand(
        string idempotencyKey,
        string aggregateId,
        string sessionKey,
        string applicationKey,
        string correlationId,
        string sagaProcessKey,
        string userEmail = "")
    {
        IdempotencyKey = idempotencyKey;
        AggregateId = aggregateId;
        SessionKey = sessionKey;
        ApplicationKey = applicationKey;
        SagaProcessKey = sagaProcessKey;
        CorrelationKey = correlationId;
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