using Knab.QuoteService.Application.Common.QueryResult;
using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using MediatR;

namespace Knab.QuoteService.Application.UseCases.GetQuote;

public class GetQuoteQueryHandler : IRequestHandler<GetQuoteQuery, IQueryResult>
{
    private readonly ICryptoCurrencyService _cryptoCurrencyService;
    private readonly ICurrencyExchangeService _currencyExchangeService;

    public GetQuoteQueryHandler(ICryptoCurrencyService cryptoCurrencyService, ICurrencyExchangeService currencyExchangeService)
    {
        _cryptoCurrencyService = cryptoCurrencyService;
        _currencyExchangeService = currencyExchangeService;
    }

    public async Task<IQueryResult> Handle(GetQuoteQuery request, CancellationToken cancellationToken)
    {
        var cryptoCurrency = await _cryptoCurrencyService.GetCryptoCurrencyAsync(request.Currency.Code);
        var exchangeRates = await _currencyExchangeService.GetExchangeRateAsync(cryptoCurrency.QuotePrice,
            targetCurrencies:
            new[]
            {
                Currency.Aud,
                Currency.Brl,
                Currency.Eur,
                Currency.Gbp,
                Currency.Usd
            });

        var quote = new Quote(cryptoCurrency.Name, cryptoCurrency.Symbol, cryptoCurrency.QuotePrice);
        foreach (var exchangeRate in exchangeRates)
        {
            quote.AddExchangeRate(exchangeRate);
        }

        return new GetQuoteQueryStandardResult(quote);
    }
}