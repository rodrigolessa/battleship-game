namespace frm.Infrastructure.Messaging;

public class MessageEnvelope<T>
{
    public string MessageId { get; set; } = Guid.NewGuid().ToString();
    public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    public T Payload { get; set; }

    public MessageEnvelope(T payload)
    {
        Payload = payload;
    }
    
    // TODO: Feature:
    // Retries / Dead-lettering =	Use x-dead-letter-exchange and x-dead-letter-routing-key
    // Tracing / Correlation    =  	Add CorrelationId to MessageEnvelope and pass via headers
    // Event Versioning         =  	Add EventType and EventVersion to the envelope
    // Outbox Pattern           = 	Use a DB table to persist events before publishing them
}