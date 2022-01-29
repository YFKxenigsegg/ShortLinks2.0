using ShortLinks.Kernel.Models;

namespace ShortLinks.Kernel.Exceptions;

public class BadRequestException : Exception
{
    private readonly Error? _error;

    public BadRequestException() : base() { }

    public BadRequestException(string message, string description) : base(description) =>
        _error = new()
        {
            Message = message,
            Description = description
        };

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Exception innerException) 
        : base(message, innerException) { }

    public bool IsError() => _error != null;

    public Error GetError() => _error!;
}
