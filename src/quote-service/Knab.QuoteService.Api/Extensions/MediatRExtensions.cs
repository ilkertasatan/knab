using Knab.QuoteService.Application.UseCases.GetQuote;
using MediatR;

namespace Knab.QuoteService.Api.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetQuoteQuery).Assembly);
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidatorBehavior<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
            
        return services;
    }
}