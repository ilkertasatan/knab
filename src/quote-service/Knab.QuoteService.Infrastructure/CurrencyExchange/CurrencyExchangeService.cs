using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CurrencyExchange;

public class CurrencyExchangeService : ICurrencyExchangeService
{
    private const string HttpClientName = "CurrencyExchange";
    
    private readonly IHttpClientFactory _httpClientFactory;

    public CurrencyExchangeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<ICollection<Price>> GetExchangeRate(Price actualPrice, ICollection<Currency> targetCurrencies)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        var response = await httpClient.GetAsync($"latest?symbols={string.Join(',', targetCurrencies)}&base={actualPrice.Currency.Code}");
        
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var rates = JsonNode.Parse(responseJson);
        
        ICollection<Price> prices = new List<Price>();
        foreach (var currency in targetCurrencies)
        {
            var rate = rates!["rates"]![currency.Code]!.GetValue<decimal>();
            prices.Add(actualPrice.Convert(rate, currency));
        }
        
        return prices;
    }
}