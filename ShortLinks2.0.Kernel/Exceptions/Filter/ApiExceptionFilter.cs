using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ShortLinks.Kernel.Exceptions.Filter;
public class ApiExceptionFilter : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilter()
    {
        //Register exception and handler
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(NotFoundException), HandleNotFoundException},
            { typeof(ValidationException),  HandleValidationException},
            { typeof(BadRequestException), HandleBadRequestException},
            { typeof(ExceptionBase), HandleExceptionBase}
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var detailt = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Extensions = { { "exception", context.Exception.Message }, { "stacktrace", context.Exception.StackTrace } }
        };
        context.Result = new ObjectResult(detailt);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = context.Exception as NotFoundException;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception!.Message
        };

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = context.Exception as ValidationException;

        var details = new ValidationProblemDetails(exception!.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleBadRequestException(ExceptionContext context)
    {
        var exception = context.Exception as BadRequestException;

        if (exception != null && exception.IsError())
            context.Result = new BadRequestObjectResult(exception.GetError());
        else
        {
            var detailts = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception!.Message
            };

            context.Result = new BadRequestObjectResult(detailts);
        }

        context.ExceptionHandled = true;
    }

    private void HandleExceptionBase(ExceptionContext context)
    {
        var exception = context.Exception as ExceptionBase;

        ProblemDetails details;
        switch (exception?.StatusCode)
        {
            case HttpStatusCode.NotFound:
                details = new()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    Title = "The specified resource was not found.",
                    Detail = exception.Message
                };
                context.Result = new NotFoundObjectResult(details);
                context.ExceptionHandled = true;
                break;
            case HttpStatusCode.BadRequest:
                details = new()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = exception.Message
                };
                context.Result = new BadRequestObjectResult(details);
                context.ExceptionHandled = true;
                break;
            case HttpStatusCode.Forbidden:
                details = new()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = exception.Message
                };
                context.Result = new ObjectResult(details);
                context.ExceptionHandled = true;
                break;
        }
    }
}
