using Knab.QuoteService.Domain;

namespace Knab.QuoteService.Application.UseCases.GetQuote;

public interface IOutputPort
{
    void NotFound();
    void BadRequest(IDictionary<string, string[]> errors);
    void Ok(Quote quote);
}