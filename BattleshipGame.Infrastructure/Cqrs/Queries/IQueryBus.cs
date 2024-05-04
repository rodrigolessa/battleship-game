using BattleshipGame.Infrastructure.Cqrs.Queries.Results;

namespace BattleshipGame.Infrastructure.Cqrs.Queries;

public interface IQueryBus
{
    Task<TResponse> Send<TQuery, TResponse>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResponse>;

    Task<PagedResult<TResponse>> SendPagedQuery<TQuery, TResponse>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : PagedQuery<TResponse> where TResponse : class;

    Task<PagedResult<TResponse>> SendOrderedPagedQuery<TQuery, TResponse>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : OrderedPagedQuery<TResponse> where TResponse : class;
}