using BattleshipGame.Infrastructure.Cqrs.Queries.Specifications;

namespace BattleshipGame.Infrastructure.Cqrs.Queries;

public interface IPagedQuery<T> where T : class
{
    ISpecification<T> Specification { get; set; }
    int PageSize { get; set; }
    int PageNumber { get; set; }
}