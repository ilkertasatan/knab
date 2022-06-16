using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CurrencyExchange;

public sealed class CurrencyExchangeServiceFake : ICurrencyExchangeService
{
    public Task<ICollection<Money>> GetExchangeRateAsync(Money actualMoney, ICollection<Currency> targetCurrencies)
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
        
        ICollection<Money> prices = new List<Money>();
        foreach (var currency in targetCurrencies)
        {
            var rate = rates!["rates"]![currency.Code]!.GetValue<decimal>();
            prices.Add(actualMoney.Convert(rate, currency));
        }
        
        return Task.FromResult(prices);
    }
}