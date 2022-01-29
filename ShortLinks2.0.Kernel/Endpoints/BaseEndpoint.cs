using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ShortLinks.Kernel.Endpoints;
public class BaseEndpoint<TRequest, TResponse, TEndpoint> : BaseAsyncEndpoint<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private IMediator _mediator = null!;
    private ILogger _logger = null!;

    protected IMediator Mediator => (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()!)!;
    protected ILogger Logger => (_logger ??= HttpContext.RequestServices.GetService<ILogger<TEndpoint>>()!)!;

    public override async Task<ActionResult<TResponse>> HandleAsync(TRequest command, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"{typeof(TRequest).Name}: {JsonConvert.SerializeObject(command)}");
        return await Mediator.Send(command, cancellationToken);
    }
}
