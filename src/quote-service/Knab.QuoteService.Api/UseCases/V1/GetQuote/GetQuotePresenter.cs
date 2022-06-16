using Knab.QuoteService.Application.UseCases.GetQuote;
using Knab.QuoteService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Knab.QuoteService.Api.UseCases.V1.GetQuote;

public sealed class GetQuotePresenter : IOutputPort
{
    public IActionResult ViewModel { get; private set; }

    public void NotFound()
    {
        ViewModel = new NotFoundResult();
    }

    public void BadRequest(IDictionary<string, string[]> errors)
    {
        ViewModel = new BadRequestObjectResult(new ValidationProblemDetails(errors));
    }
    
    public void Ok(Quote quote)
    {
        var response = new GetQuoteResponse(
            quote.Name,
            quote.Symbol.Code,
            new QuoteModel(quote.Money.Amount, quote.Money.Currency.Code));

        foreach (var (amount, currency) in quote.ExchangeRates)
        {
            response.ExchangeRates.Add(new ExchangeRateModel(amount, currency.Code));
        }

        ViewModel = new OkObjectResult(response);
    }
}