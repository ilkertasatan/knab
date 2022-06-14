using Knab.QuoteService.Api.UseCases.GetQuote;

namespace Knab.QuoteService.Api.Extensions;

public static class UseCaseExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuoteUseCase(configuration);
        return services;
    }
}