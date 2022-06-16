using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CryptoCurrency;

public sealed class CryptoCurrencyService : ICryptoCurrencyService
{
    public const string HttpClientName = "CryptoCurrency";

    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoCurrencyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Quote> GetCryptoCurrencyAsync(Symbol symbol)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        var response = await httpClient.GetAsync($"quotes/latest?symbol={symbol}");

        if (!response.IsSuccessStatusCode)
            return Quote.None;
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var rates = JsonNode.Parse(responseJson);

        var name = string.Empty;
        decimal price = 0;
        var data = rates!["data"]![symbol.Code.ToUpper()];
        if (data != null && data.AsArray().Count > 0)
        {
            name = data![0]!["name"]!.GetValue<string>();
            price = data[0]!["quote"]![Currency.Usd.Code]!["price"]!.GetValue<decimal>();    
        }

        return new Quote(name, symbol, new Money(price, Currency.Usd));
    }
}