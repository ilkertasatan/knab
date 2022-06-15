using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CurrencyExchange;

public class CurrencyExchangeServiceFake : ICurrencyExchangeService
{
    public Task<ICollection<Price>> GetExchangeRateAsync(Price actualPrice, ICollection<Currency> targetCurrencies)
    {
        const string responseJson = @"{
            'base': 'USD',
            'date': '2022-06-14',
            'rates': {
                'AUD': 1.457322,
                'BRL': 5.139496,
                'EUR': 0.96034,
                'GBP': 0.83501,
                'USD': 1
            },
        'success': true}";
        var rates = JsonNode.Parse(responseJson.Replace("'","\""));
        
        ICollection<Price> prices = new List<Price>();
        foreach (var currency in targetCurrencies)
        {
            var rate = rates!["rates"]![currency.Code]!.GetValue<decimal>();
            prices.Add(actualPrice.Convert(rate, currency));
        }
        
        return Task.FromResult(prices);
    }
}