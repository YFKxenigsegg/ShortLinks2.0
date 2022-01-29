using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ShortLinks.Kernel.Behaviors;
public class RequestPerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;

    public RequestPerformanceBehavior(ILogger<TRequest> logger) => (_logger, _timer) = (logger, new());

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds < 500) return response;

        var requestName = typeof(TRequest).Name;

        _logger.LogWarning("Long Running Request: {Name} {elapsedMilliseconds} {Request} milliseconds",
            requestName, elapsedMilliseconds, JsonConvert.SerializeObject(request));

        return response;
    }
}
