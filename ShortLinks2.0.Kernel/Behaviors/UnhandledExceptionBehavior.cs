using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ShortLinks.Kernel.Behaviors;
public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "Request: Unhandled Excepton for request {Name} {Request}", requestName, JsonConvert.SerializeObject(request));

            throw;
        }
    }
}
