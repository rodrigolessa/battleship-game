using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace frm.Infrastructure.Cqrs.Requests;

[ExcludeFromCodeCoverage]
public class RequestProcessor : IRequestProcessor
{
    private readonly IMediator _mediator;

    public RequestProcessor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResponse> Process<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        return _mediator.Send(request, cancellationToken);
    }
}