namespace BattleshipGame.WebApi.RequestProcessor;

public interface ICommandRequestHandler<in TRequest, TResponse> where TRequest : ICommandRequest<TResponse>
{
    void Handle(TRequest request);
}