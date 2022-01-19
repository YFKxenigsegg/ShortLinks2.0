using Microsoft.AspNetCore.Mvc;
using ShortLinks.Application.LinkManagment;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Kernel.Endpoints;

namespace ShortLinks.LinkManager.Endpoints.Link;
public class GetEndpoint : BaseEndpoint<GetRequest, LinkInfo, GetEndpoint>
{
    [HttpGet("Link/Get/")]
    public override async Task<ActionResult<LinkInfo>> HandleAsync([FromQuery] GetRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
