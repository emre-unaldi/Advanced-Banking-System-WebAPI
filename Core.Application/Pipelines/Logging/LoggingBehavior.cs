using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.Application.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerServicBase;

    public LoggingBehavior(IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServicBase)
    {
        _httpContextAccessor = httpContextAccessor;
        _loggerServicBase = loggerServicBase;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter { Type = request.GetType().Name, Value = request }
        };

        LogDetail logDetail = new()
        {
            MethodName = next.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?"
        };

        _loggerServicBase.Info(JsonSerializer.Serialize(logDetail));

        return await next();
    }
}
