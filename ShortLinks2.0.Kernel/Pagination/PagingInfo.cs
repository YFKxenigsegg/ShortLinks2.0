namespace ShortLinks.Kernel.Pagination;
public class PagingInfo : IPagingInfo
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItemCount { get; set; }
    public int PageCount
    {
        get
        {
            if (TotalItemCount > 0)
            {
                if (PageSize <= 0) PageSize = TotalItemCount;

                return (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            }
            return 0;
        }
    }

    public PagingInfo() { }

    public PagingInfo(int pageNumber, int pageSize, int totalItemCount) =>
        (PageNumber, PageSize, TotalItemCount) = (pageNumber, pageSize, totalItemCount);
}
