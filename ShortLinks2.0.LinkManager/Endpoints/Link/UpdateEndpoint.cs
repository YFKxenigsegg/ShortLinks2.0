using Microsoft.AspNetCore.Mvc;
using ShortLinks.Application.LinkManagment;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Kernel.Endpoints;

namespace ShortLinks.LinkManager.Endpoints.Link;
public class UpdateEndpoint : BaseEndpoint<UpdateRequest, LinkInfo, UpdateEndpoint>
{
    [HttpPost("Link/Update/")]
    public override async Task<ActionResult<LinkInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
