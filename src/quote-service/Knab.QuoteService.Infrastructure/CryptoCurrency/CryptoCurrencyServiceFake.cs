using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using System.Text.Json.Nodes;

namespace Knab.QuoteService.Infrastructure.CryptoCurrency;

public sealed class CryptoCurrencyServiceFake : ICryptoCurrencyService
{
    public async Task<Quote> GetCryptoCurrencyAsync(Symbol symbol)
    {
        const string responseJson = @"{
            'data':{
                'BTC': [
                    {
                        'name': 'Bitcoin',
                        'symbol': 'BTC',
                        'quote':{
                            'USD':{
                                'price':21005
                            }
                        }
                    }]
                }
            }";
        var rates = JsonNode.Parse(responseJson.Replace("'","\""));

        var data = rates!["data"]![symbol.Code.ToUpper()];
        var name = data![0]!["name"]!.GetValue<string>();
        var price = data[0]!["quote"]![Currency.Usd.Code]!["price"]!.GetValue<decimal>();

        return await Task.FromResult(new Quote(name, symbol, new Money(price, Currency.Usd)));
    }
}