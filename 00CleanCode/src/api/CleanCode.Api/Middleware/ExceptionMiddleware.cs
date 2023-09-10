using CleanCode.Api.Models;
using CleanCode.Core.Application.Exceptions;
using System.Net;

namespace CleanCode.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badHttpRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badHttpRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badHttpRequestException.InnerException?.Message,
                    Type = nameof(BadHttpRequestException),
                    Errors = badHttpRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = notFound.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFound.InnerException?.Message,
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = ex.StackTrace,
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(problem);
    }
}
