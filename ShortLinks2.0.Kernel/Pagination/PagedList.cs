namespace ShortLinks.Kernel.Pagination;
public class PagedList<T> : IPagedList<T>
{
    public PagingInfo PagingInfo { get; }
    public List<T> Result { get; }

    public PagedList() => (PagingInfo, Result) = (new(), new());

    public PagedList(IQueryable<T> items, int pageNumber, int pageSize)
    {
        var totalItemCount = items?.Count() ?? 0;

        if (pageSize <= 0) pageSize = totalItemCount;
        if (pageSize <= 0) pageSize = 0;

        PagingInfo = new PagingInfo(pageNumber, pageSize, totalItemCount);

        if (pageSize == totalItemCount)
            Result = items?.ToList()!;
        else
            Result = items?.Skip(pageNumber * pageSize).Take(pageSize).ToList()!;
    }
}
