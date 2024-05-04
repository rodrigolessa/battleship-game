using System.Collections;

namespace BattleshipGame.Infrastructure.Cqrs.Queries.Results;

public interface IPagedResult
{
    IEnumerable GetRecords();
    PagedResultDetails GetPagination();
}