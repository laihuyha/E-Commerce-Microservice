using System;
using System.Text.Json;
using System.Threading.Tasks;
using BuildingBlocks.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Middlewares
{
    /// <summary>
    /// More about Handle errors: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0
    /// </summary>
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
    {
        private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
        private readonly ILogger<ExceptionMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IHostEnvironment _host = host ?? throw new ArgumentNullException(nameof(host));

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred.");

            var (title, status, details) = GetExceptionDetails(exception);

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = status,
                Detail = _host.IsDevelopment() ? BuildDetailString(details, exception) : null,
                Instance = _host.IsDevelopment() ? exception.Source : null
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)status;

            var json = JsonSerializer.Serialize(problemDetails, JsonSerializerOptions);
            return context.Response.WriteAsync(json);
        }

        private (string title, int? status, string details) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                NotFoundException => (exception.GetType().Name, StatusCodes.Status404NotFound, exception.Message),
                DbErrorException => (exception.GetType().Name, StatusCodes.Status500InternalServerError, exception.Message),
                InternalException => (exception.GetType().Name, StatusCodes.Status500InternalServerError, exception.Message),
                BadRequestException => (exception.GetType().Name, StatusCodes.Status400BadRequest, exception.Message),
                _ => (exception.GetType().Name, StatusCodes.Status500InternalServerError, exception.Message)
            };
        }

        private string BuildDetailString(string details, Exception exception)
        {
            return $"{details}\nException: ====> {exception.StackTrace}";
        }
    }
}
