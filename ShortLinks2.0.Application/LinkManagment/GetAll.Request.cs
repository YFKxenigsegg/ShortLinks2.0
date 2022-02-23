using MediatR;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Kernel.Pagination;

namespace ShortLinks.Application.LinkManagment;
public class GetAllRequest : GetAllFilter, IRequest<IPagedList<LinkInfo>> { }
