using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CryptoCurrency;

public class CryptoCurrencyService : ICryptoCurrencyService
{
    public const string HttpClientName = "CryptoCurrency";

    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoCurrencyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Quote> GetCryptoCurrencyAsync(string symbol)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        var response = await httpClient.GetAsync($"quotes/latest?symbol={symbol}");
        
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var rates = JsonNode.Parse(responseJson);

        var data = rates!["data"]![symbol];
        var name = data![0]!["name"]!.GetValue<string>();
        var price = data[0]!["quote"]![Currency.Usd.Code]!["price"]!.GetValue<decimal>();

        return new Quote(name, symbol, new Price(price, Currency.Usd));
    }
}