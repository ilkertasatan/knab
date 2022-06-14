using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Domain;

public class Quote
{
    private Quote() { }

    public Quote(string name, string symbol, Price price)
    {
        Name = name;
        Symbol = symbol;
        Price = price;
    }

    public string Name { get; }

    public string Symbol { get; }

    public Price Price { get; }
}