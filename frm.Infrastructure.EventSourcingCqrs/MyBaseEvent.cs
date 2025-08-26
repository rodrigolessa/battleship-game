using System.ComponentModel.Design;
using BattleshipGame.Infrastructure.Abstractions;
using frm.Infrastructure.EventSourcing.Events;
using MediatR;

namespace frm.Infrastructure.EventSourcingCqrs;

public class MyBaseEvent : IEvent, IRequest 
{
    public string EventKey { get; }
    public string IdempotencyKey { get; set; }
    public string AggregateId { get; set; }
    public string SessionKey { get; set; }
    public string ApplicationKey { get; set; }
    public string SagaProcessKey { get; set; }
    public string CorrelationKey { get; set; }
    public string UserEmail { get; set; }
    public DateTime EventCommittedTimestamp { get; set; }
    
    public MyBaseEvent(string eventKey, string idempotencyKey, string aggregateId, string sessionKey, string applicationKey, string sagaProcessKey, string correlationKey, IClock clock, string userEmail = "")
    {
        EventKey = eventKey;
        IdempotencyKey = idempotencyKey;
        AggregateId = aggregateId;
        SessionKey = sessionKey;
        ApplicationKey = applicationKey;
        SagaProcessKey = sagaProcessKey;
        CorrelationKey = correlationKey;
        UserEmail = userEmail;
        EventCommittedTimestamp = clock.UtcNow();
    }
}