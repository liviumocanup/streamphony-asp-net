namespace Streamphony.Application.Models;

public class PaginatedResult<T>(IEnumerable<T> items, int totalRecords)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalRecords { get; set; } = totalRecords;
}
