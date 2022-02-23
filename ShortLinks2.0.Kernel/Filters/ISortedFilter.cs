namespace ShortLinks.Kernel.Filters;
public interface ISortedFilter
{
    string SortColumn { get; set; }
    bool IsAscending { get; set; }
}
