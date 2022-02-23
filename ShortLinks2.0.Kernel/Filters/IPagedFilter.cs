namespace ShortLinks.Kernel.Filters;
public interface IPagedFilter
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
