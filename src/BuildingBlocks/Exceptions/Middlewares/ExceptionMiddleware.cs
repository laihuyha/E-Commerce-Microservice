using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Middlewares;

/// <summary>
/// Represents a middleware for handling exceptions in an ASP.NET Core application.
/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _host;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger for logging exceptions.</param>
    /// <param name="host">The host environment for determining if the application is in development mode.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _host = host ?? throw new ArgumentNullException(nameof(host));
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>An asynchronous task.</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Handles the exception by logging it, creating problem details, and writing the response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <returns>An asynchronous task.</returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unexpected error occurred.");

        var (title, status, details) = GetExceptionDetails(exception);

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = _host.IsDevelopment() ? BuildDetailString(details, exception) : details,
            Instance = _host.IsDevelopment() ? exception.Source : context.Request.Path
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = status ?? StatusCodes.Status500InternalServerError;

        var json = JsonSerializer.Serialize(problemDetails, JsonSerializerOptions);

        await context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Gets the exception details based on the type of exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>A tuple containing the title, status, and details of the exception.</returns>
    private static (string title, int? status, string details) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            NotFoundException => (nameof(NotFoundException), StatusCodes.Status404NotFound, exception.Message),
            DbErrorException => (nameof(DbErrorException), StatusCodes.Status500InternalServerError, exception.Message),
            InternalException => (nameof(InternalException), StatusCodes.Status500InternalServerError, exception.Message),
            BadRequestException => (nameof(BadRequestException), StatusCodes.Status400BadRequest, exception.Message),
            _ => ("UnexpectedError", StatusCodes.Status500InternalServerError, exception.Message)
        };
    }

    /// <summary>
    /// Builds the detail string for the problem details, including the exception stack trace if the application is in development mode.
    /// </summary>
    /// <param name="details">The details of the exception.</param>
    /// <param name="exception">The exception.</param>
    /// <returns>The detail string.</returns>
    private static string BuildDetailString(string details, Exception exception)
    {
        return $"{details}\nException: ====> {exception.StackTrace}";
    }
}
