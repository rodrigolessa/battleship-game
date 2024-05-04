namespace BattleshipGame.Infrastructure.Cqrs.Queries.Results;

public class PagedResultDetails
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public long TotalRecords { get; set; }

    public PagedResultDetails()
    {
    }

    public PagedResultDetails(int pageNumber, int pageSize, long totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = (int)Math.Ceiling((double)totalRecords / pageSize);
        TotalRecords = totalRecords;
    }
}