using Knab.QuoteService.Application.Common.QueryResult;
using Knab.QuoteService.Domain;

namespace Knab.QuoteService.Application.UseCases.GetQuote;

public class GetQuoteQueryStandardResult : IQueryResult
{
    public GetQuoteQueryStandardResult(Quote quote)
    {
        Quote = quote;
    }

    public Quote Quote { get; }
}