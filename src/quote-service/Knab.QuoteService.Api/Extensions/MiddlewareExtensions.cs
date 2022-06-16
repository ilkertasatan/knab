using Knab.QuoteService.Api.Middleware;

namespace Knab.QuoteService.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}