using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CurrencyExchange;

public sealed class CurrencyExchangeService : ICurrencyExchangeService
{
    public const string HttpClientName = "CurrencyExchange";

    private readonly IHttpClientFactory _httpClientFactory;

    public CurrencyExchangeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<ICollection<Money>> GetExchangeRateAsync(Money actualMoney, ICollection<Currency> targetCurrencies)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        var response = await httpClient.GetAsync($"latest?symbols={string.Join(',', targetCurrencies)}&base={actualMoney.Currency.Code}");

        if (!response.IsSuccessStatusCode)
            return (ICollection<Money>)Enumerable.Empty<Money>();
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var rates = JsonNode.Parse(responseJson);
        
        ICollection<Money> prices = new List<Money>();
        foreach (var currency in targetCurrencies)
        {
            var rate = rates!["rates"]![currency.Code]!.GetValue<decimal>();
            prices.Add(actualMoney.Convert(rate, currency));
        }
        
        return prices;
    }
}