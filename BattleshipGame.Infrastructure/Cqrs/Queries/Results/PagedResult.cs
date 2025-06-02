using System.Collections;

namespace BattleshipGame.Infrastructure.Cqrs.Queries.Results;

public class PagedResult<T> : IPagedResult where T : class
{
    private IEnumerable<T> Records { get; set; }
    private PagedResultDetails PageDetails { get; set; }
    
    public PagedResult(IEnumerable<T> records)
    {
        Records = records;
        PageDetails = new PagedResultDetails();
    }

    public IEnumerable GetRecords()
    {
        return Records;
    }

    public PagedResultDetails GetPagination()
    {
        return PageDetails;
    }
}