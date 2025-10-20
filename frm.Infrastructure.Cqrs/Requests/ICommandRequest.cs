using MediatR;

namespace frm.Infrastructure.Cqrs.Requests;

public interface ICommandRequest<out TResponse> : IRequest<TResponse>
{
}