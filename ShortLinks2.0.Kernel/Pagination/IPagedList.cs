namespace ShortLinks.Kernel.Pagination;
public interface IPagedList<T>
{
    PagingInfo PagingInfo { get; }
    List<T> Result { get; }
}
