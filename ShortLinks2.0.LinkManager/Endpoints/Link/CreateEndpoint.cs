using Microsoft.AspNetCore.Mvc;
using ShortLinks.Application.LinkManagment;
using ShortLinks.Application.LinkManagment.Models;
using ShortLinks.Kernel.Endpoints;

namespace ShortLinks.LinkManager.Endpoints.Link;
public class CreateEndpoint : BaseEndpoint<CreateRequest, LinkInfo, CreateEndpoint>
{
    [HttpPut("Link/Create")]
    public override async Task<ActionResult<LinkInfo>> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken) => 
        await base.HandleAsync(request, cancellationToken);
}
