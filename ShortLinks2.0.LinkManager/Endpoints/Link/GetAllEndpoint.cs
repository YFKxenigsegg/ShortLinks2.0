using Microsoft.AspNetCore.Mvc;
using ShortLinks.Application.LinkManagment;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Kernel.Endpoints;
using ShortLinks.Kernel.Pagination;

namespace ShortLinks.LinkManager.Endpoints.Link;
public class GetAllEndpoint : BaseEndpoint<GetAllRequest, IPagedList<LinkInfo>, GetAllEndpoint>
{
    [HttpPost("Link/GetAll/")]
    public override async Task<ActionResult<IPagedList<LinkInfo>>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
