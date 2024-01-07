using Core.CrossCuttingConcerns.Exceptions.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExcepitonMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}
