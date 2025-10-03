using System.Collections;

namespace frm.Infrastructure.Cqrs.Queries.Results;

public interface IPagedResult
{
    IEnumerable GetRecords();
    PagedResultDetails GetPagination();
}