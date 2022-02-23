namespace ShortLinks.Kernel.Filters;
public abstract class SortedPagedFilter : IPagedFilter, ISortedFilter
{
    public string? FreeText { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string SortColumn { get; set; }
    public bool IsAscending { get; set; }

    protected SortedPagedFilter() =>
        (PageNumber, PageSize, SortColumn, IsAscending) =
        (Constants.DefaultPageNumber, Constants.DefaultPageSize, Constants.DefaultSortColumn, Constants.DefaultIsAscending);
}
