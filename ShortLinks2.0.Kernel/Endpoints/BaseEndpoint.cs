using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ShortLinks.Kernel.Endpoints;
public class BaseEndpoint<TRequest, TResponse, TEndpoint> : BaseAsyncEndpoint<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private IMediator _mediator = null!;

    protected IMediator Mediator => (_mediator??= HttpContext.RequestServices.GetService<IMediator>()!)!;

    public override async Task<ActionResult<TResponse>> HandleAsync (TRequest command, CancellationToken cancellationToken)
    {
        //Logging
        return await Mediator.Send(command, cancellationToken);
    }
}
