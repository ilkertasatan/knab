using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Infrastructure.CurrencyExchange;

namespace Knab.QuoteService.Api.UseCases.GetQuote;

public static class Dependencies
{
    public static IServiceCollection AddQuoteUseCase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ICryptoCurrencyService>("CryptoCurrency", client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalApi:CryptoCurrencyService:Url"]);
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration["ExternalApi:CryptoCurrencyService:ApiKey"]);
        });
        
        services.AddHttpClient<ICurrencyExchangeService, CurrencyExchangeService>("CurrencyExchange", client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalApi:ExchangeRate:Url"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("apikey", configuration["ExternalApi:ExchangeRate:ApiKey"]);
        });
        services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();
        
        return services;
    }
}