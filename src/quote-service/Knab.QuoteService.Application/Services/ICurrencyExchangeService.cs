﻿using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Application.Services;

public interface ICurrencyExchangeService
{
    Task<ICollection<Price>> GetExchangeRateAsync(Price actualPrice, ICollection<Currency> targetCurrencies);
}