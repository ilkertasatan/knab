using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Application.UseCases.GetQuote;

public interface IGetQuoteUseCase
{
    Task Execute(Symbol symbol, IOutputPort outputPort);
}