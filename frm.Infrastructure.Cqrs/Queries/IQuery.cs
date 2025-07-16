using MediatR;

namespace frm.Infrastructure.Cqrs.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}