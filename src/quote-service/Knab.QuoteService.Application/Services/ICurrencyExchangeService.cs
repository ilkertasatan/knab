using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Application.Services;

public interface ICurrencyExchangeService
{
    Task<ICollection<Money>> GetExchangeRateAsync(Money actualMoney, ICollection<Currency> targetCurrencies);
}