namespace Streamphony.Application.Common;

public class PaginatedResult<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalRecords { get; set; }
}
