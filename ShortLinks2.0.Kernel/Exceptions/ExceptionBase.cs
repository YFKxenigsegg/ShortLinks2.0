using System.Net;

namespace ShortLinks.Kernel.Exceptions;
public class ExceptionBase : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ExceptionBase(HttpStatusCode statusCode, string message) : base(message) =>
        StatusCode = statusCode;
    public ExceptionBase(HttpStatusCode statusCode, string message, Exception innerException) 
        : base(message, innerException) =>
        StatusCode = statusCode;
}
