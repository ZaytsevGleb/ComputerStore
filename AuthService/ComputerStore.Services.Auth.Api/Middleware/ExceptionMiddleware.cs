using System.ComponentModel.DataAnnotations;
using System.Net;
using ComputerStore.Services.Auth.Api.Dtos;
using ComputerStore.Services.Auth.BusinessLogic.Errors;
using ComputerStore.Services.Auth.BusinessLogic.Exceptions;
using Newtonsoft.Json;

namespace ComputerStore.Services.Auth.Api.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = badRequestException.Message });
                break;
            case ForbiddenException forbiddenException:
                code = HttpStatusCode.Forbidden;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = forbiddenException.Message });
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new ErrorDto { Message = exception.Message, Details = exception.StackTrace });
                _logger.LogError(exception, exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        await context.Response.WriteAsync(result);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}