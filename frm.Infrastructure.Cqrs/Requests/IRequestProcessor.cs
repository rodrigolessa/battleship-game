using MediatR;

namespace frm.Infrastructure.Cqrs.Requests;

public interface IRequestProcessor
{
    Task<TResponse> Process<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>;
}