using System.Linq.Expressions;

namespace frm.Infrastructure.Cqrs.Queries;

public class OrderedPagedQuery<T> : PagedQuery<T> where T : class
{
    public Expression<Func<T, object>> SortingCriteria { get; set; }
}