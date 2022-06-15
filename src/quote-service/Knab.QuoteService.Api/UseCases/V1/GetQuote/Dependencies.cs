using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Application.UseCases.GetQuote;
using Knab.QuoteService.Infrastructure.CryptoCurrency;
using Knab.QuoteService.Infrastructure.CurrencyExchange;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace Knab.QuoteService.Api.UseCases.V1.GetQuote;

public static class Dependencies
{
    public static IServiceCollection AddQuoteUseCase(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpClient<ICryptoCurrencyService>(CryptoCurrencyService.HttpClientName, client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalServices:CryptoCurrency:Url"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration["ExternalServices:CryptoCurrency:ApiKey"]);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());
        
        services
            .AddHttpClient<ICurrencyExchangeService, CurrencyExchangeService>(CurrencyExchangeService.HttpClientName,
                client =>
                {
                    client.BaseAddress = new Uri(configuration["ExternalServices:ExchangeRate:Url"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("apikey", configuration["ExternalServices:ExchangeRate:ApiKey"]);
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());
        
        services.AddSingleton<ICryptoCurrencyService, CryptoCurrencyService>();
        services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();
        
        services.AddScoped<IGetQuoteUseCase, GetQuoteUseCase>();
        services.AddScoped<IOutputPort, GetQuotePresenter>();
        services.AddScoped<GetQuotePresenter, GetQuotePresenter>();
        
        return services;
    }
    
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.TooManyRequests)
            .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            .OrResult(httpResponseMessage => httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(retryCount: 5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}