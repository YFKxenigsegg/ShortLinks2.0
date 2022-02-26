using ShortLinks.Kernel.Filters;
using ShortLinks.Kernel.Pagination;

namespace ShortLinks.Kernel.Extensions;
public static class QueryableExtensions
{
    public static IPagedList<T> ApplyPaging<T, TPagedFilter>(this IQueryable<T> items, TPagedFilter filter)
        where TPagedFilter : IPagedFilter => new PagedList<T>(items, filter.PageNumber, filter.PageSize);
}
