using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Application.Services;

public interface ICryptoCurrencyService
{
    Task<Quote> GetCryptoCurrencyAsync(Symbol symbol);
}
