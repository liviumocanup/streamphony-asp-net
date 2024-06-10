namespace Streamphony.Application.Common;

public class PagedRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string ColumnNameForSorting { get; set; } = default!;
    public string SortDirection { get; set; } = default!;
    public RequestFilters RequestFilters { get; set; } = new();
}
