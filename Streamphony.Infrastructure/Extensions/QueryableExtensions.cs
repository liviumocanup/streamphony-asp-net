using System.Linq.Dynamic.Core;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Common;
using Streamphony.Domain.Models;

namespace Streamphony.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static async Task<(PaginatedResult<TDto>, IEnumerable<T>)> CreatePaginatedResultAsync<T, TDto>(
        this IQueryable<T> query, PagedRequest pagedRequest, CancellationToken cancellationToken)
        where T : BaseEntity
        where TDto : class
    {
        query = query.ApplyFilters(pagedRequest);
        query = query.Sort(pagedRequest);

        var total = await query.CountAsync(cancellationToken);

        query = query.Paginate(pagedRequest);

        var listResult = await query.ToListAsync(cancellationToken);

        return (new PaginatedResult<TDto>
        {
            PageNumber = pagedRequest.PageNumber,
            PageSize = pagedRequest.PageSize,
            TotalRecords = total
        }, listResult);
    }

    private static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PagedRequest pagedRequest)
    {
        var predicate = new StringBuilder();
        var requestFilters = pagedRequest.RequestFilters;
        for (var i = 0; i < requestFilters.Filters.Count; i++)
        {
            if (i > 0) predicate.Append($" {requestFilters.LogicalOperator} ");
            predicate.Append(requestFilters.Filters[i].Path + $".{nameof(string.Contains)}(@{i})");
        }

        if (!requestFilters.Filters.Any()) return query;
        var propertyValues = requestFilters.Filters.Select(filter => filter.Value).ToArray();

        query = query.Where(predicate.ToString(), propertyValues);

        return query;
    }

    private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedRequest pagedRequest)
    {
        var entities = query.Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize)
            .Take(pagedRequest.PageSize);
        return entities;
    }

    private static IQueryable<T> Sort<T>(this IQueryable<T> query, PagedRequest pagedRequest)
    {
        query = !string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting)
            ? query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection)
            : query.OrderBy("Id");
        return query;
    }
}
