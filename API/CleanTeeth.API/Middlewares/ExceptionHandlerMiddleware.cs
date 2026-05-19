using System.Net;
using System.Text.Json;
using CleanTeeth.Application.Exceptions;

namespace CleanTeeth.API.Middlewares;

internal class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {

            await HandleException(context, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var result = string.Empty;

        switch (exception)
        {
            case NotFoundExcepetion:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case ApplicationValidationException validationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.ValidationErrors);
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;
        return context.Response.WriteAsync(result);
    }
}

internal static class ExceptionHandlerMiddlewareExtensions
{
    extension(IApplicationBuilder builder)
    {
        internal IApplicationBuilder UseErrorExceptionHandler()
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}