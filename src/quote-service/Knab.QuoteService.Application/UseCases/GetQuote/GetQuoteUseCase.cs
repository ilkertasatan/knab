using FluentValidation;
using Knab.QuoteService.Application.Extensions;
using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Application.UseCases.GetQuote;

public sealed class GetQuoteUseCase : IGetQuoteUseCase
{
    private readonly ICryptoCurrencyService _cryptoCurrencyService;
    private readonly ICurrencyExchangeService _currencyExchangeService;
    private readonly IValidator<Symbol> _validator;

    public GetQuoteUseCase(
        ICryptoCurrencyService cryptoCurrencyService,
        ICurrencyExchangeService currencyExchangeService,
        IValidator<Symbol> validator)
    {
        _cryptoCurrencyService = cryptoCurrencyService;
        _currencyExchangeService = currencyExchangeService;
        _validator = validator;
    }

    public async Task Execute(Symbol symbol, IOutputPort outputPort)
    {
        var validation = await _validator.ValidateAsync(symbol);
        if (!validation.IsValid)
        {
            outputPort.BadRequest(validation.Errors.ToDictionary());
            return;
        }
        
        var cryptoCurrency = await _cryptoCurrencyService.GetCryptoCurrencyAsync(symbol);
        if (cryptoCurrency.IsEmpty())
        {
            outputPort.NotFound();
            return;
        }
        
        var exchangeRates = await _currencyExchangeService.GetExchangeRateAsync(cryptoCurrency.Price,
            targetCurrencies: new[]
            {
                Currency.Aud,
                Currency.Brl,
                Currency.Eur,
                Currency.Gbp,
                Currency.Usd
            });

        var quote = new Quote(cryptoCurrency.Name, cryptoCurrency.Symbol, cryptoCurrency.Price);
        foreach (var exchangeRate in exchangeRates)
        {
            quote.AddExchangeRate(exchangeRate);
        }

        outputPort.Ok(quote);
    }
}