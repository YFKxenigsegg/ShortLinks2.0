using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShortLinks.Application.LinkManagment;
using ShortLinks.Kernel.Endpoints;

namespace ShortLinks.LinkManager.Endpoints.Link;
public class DeleteEndpoint : BaseEndpoint<DeleteRequest, Unit, DeleteEndpoint>
{
    [HttpPost("Link/Delete/")]
    public override async Task<ActionResult<Unit>> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
