using frm.Infrastructure.Cqrs.Queries.Results;
using frm.Infrastructure.Cqrs.Queries.Specifications;

namespace frm.Infrastructure.Cqrs.Queries;

public class PagedQuery<T> : IPagedQuery<T>, IQuery<PagedResult<T>> where T : class
{
    public ISpecification<T> Specification { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}