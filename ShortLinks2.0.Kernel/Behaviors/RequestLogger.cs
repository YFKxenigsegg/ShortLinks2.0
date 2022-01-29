using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ShortLinks.Kernel.Behaviors;
public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest> 
    where TRequest : notnull
{
    private readonly ILogger _logger;

    public RequestLogger(ILogger<TRequest> logger) => _logger = logger;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogDebug($"Request {requestName} {JsonConvert.SerializeObject(request)}");

        return Task.CompletedTask;
    }
}
