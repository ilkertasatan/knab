using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Infrastructure.CryptoCurrency;
using Knab.QuoteService.Infrastructure.CurrencyExchange;

namespace Knab.QuoteService.Api.UseCases.GetQuote;

public static class Dependencies
{
    public static IServiceCollection AddQuoteUseCase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ICryptoCurrencyService>(CryptoCurrencyService.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:CryptoCurrency:Url"]);
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration["ExternalServices:CryptoCurrency:ApiKey"]);
        });
        services.AddSingleton<ICryptoCurrencyService, CryptoCurrencyService>();
        
        services.AddHttpClient<ICurrencyExchangeService, CurrencyExchangeService>(CurrencyExchangeService.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(configuration["ExternalServices:ExchangeRate:Url"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("apikey", configuration["ExternalServices:ExchangeRate:ApiKey"]);
        });
        services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();
        
        return services;
    }
}