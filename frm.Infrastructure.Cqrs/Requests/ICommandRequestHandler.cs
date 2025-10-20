namespace frm.Infrastructure.Cqrs.Requests;

public interface ICommandRequestHandler<in TRequest, TResponse> where TRequest : ICommandRequest<TResponse>
{
    void Handle(TRequest request);
}