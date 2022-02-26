using ShortLinks.Kernel.Filters;

namespace ShortLinks.Application.LinkManagment;
public class GetAllFilter : SortedPagedFilter
{
    public GetAllFilter() => IsAscending = true;
}
