using System.Diagnostics;

namespace ComputerStore.Services.Auth.Api.Middleware;

public sealed class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var uri = BuildUriFromRequest(context.Request);
        var method = context.Request.Method;

        _logger.LogInformation("Request {uri} ({method}) started", uri, method);

        var timer = new Stopwatch();
        timer.Start();

        try
        {
            await _next(context);
        }
        finally
        {
            timer.Stop();
        }

        _logger.LogInformation(
            "Request {uri} ({method}) finished ( Status code: {statusCode}, Duration: {duration} ms )",
            uri,
            method,
            context.Response.StatusCode,
            timer.ElapsedMilliseconds);
    }

    private Uri BuildUriFromRequest(HttpRequest request)
    {
        var builder = new UriBuilder
        {
            Scheme = request.Scheme,
            Host = request.Host.Host
        };

        if (request.Host.Port.HasValue)
        {
            builder.Port = request.Host.Port.Value;
        }

        builder.Path = request.Path.ToString();
        builder.Query = request.QueryString.ToString();

        return builder.Uri;
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}