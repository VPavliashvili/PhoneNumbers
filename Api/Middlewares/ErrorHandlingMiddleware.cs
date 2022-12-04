using Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly string _unicId;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, LoggingIdWrapper loggingId)
    {
        _logger = logger;
        _unicId = loggingId.UnicId;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex, StatusCodes.Status500InternalServerError, 
                        "Server error", "Server error detected", ex.Message);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex, int statusCode, string type, string title, string details)
    {
        _logger.LogError(ex, $"unicId -> {_unicId}, ex.Message");
        context.Response.StatusCode = statusCode;

        var problem = new ProblemDetails()
        {
            Status = statusCode,
            Type = type,
            Title = title,
            Detail = details,
        };

        await context.Response.WriteAsJsonAsync(problem);

        context.Response.ContentType = "application/json";
    }
}
