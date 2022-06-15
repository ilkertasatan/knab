using Knab.QuoteService.Domain;

namespace Knab.QuoteService.Application.Services;

public interface ICryptoCurrencyService
{
    Task<Quote> GetCryptoCurrencyAsync(string symbol);
}
