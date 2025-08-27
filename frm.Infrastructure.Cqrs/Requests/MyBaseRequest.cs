using MediatR;

namespace frm.Infrastructure.Cqrs.Requests;

public class MyBaseRequest<TResponse> : IRequest<TResponse>
{
    public string? ClientApplication { get; set; }
    public string? IpAddress { get; set; }
    public string? UserEmail { get; set; }
    public string? IdempotencyKey { get; set; }
    public string? CorrelationKey { get; set; }
    public string? SagaProcessKey { get; set; }
}