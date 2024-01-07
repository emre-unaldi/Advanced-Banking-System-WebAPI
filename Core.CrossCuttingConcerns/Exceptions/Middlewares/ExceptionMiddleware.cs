using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerServiceBase;

    public ExceptionMiddleware(
        RequestDelegate next, 
        IHttpContextAccessor httpContextAccessor, 
        LoggerServiceBase loggerServiceBase
    )
    {
        _next = next;
        _httpExceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExcepitonAsync(context.Response, exception);
        }
    }

    private Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter{ Type = context.GetType().Name, Value = exception.ToString() }
        };

        LogDetailWithException logDetail = new()
        {
            ExceptionMessage = exception.Message,
            MethodName = _next.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Emre"
        };

        _loggerServiceBase.Error(JsonSerializer.Serialize(logDetail));

        return Task.CompletedTask;
    }

    private Task HandleExcepitonAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;

        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }
}
