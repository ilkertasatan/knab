using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Domain;

public class Quote
{
    public static readonly Quote None = new();
    
    private readonly List<Price> _exchangeRates;
    private Quote() { }

    public Quote(string name, Symbol symbol, Price price)
    {
        Name = name;
        Symbol = symbol;
        Price = price;
        _exchangeRates = new List<Price>();
    }

    public string Name { get; }

    public Symbol Symbol { get; }

    public Price Price { get; }

    public IReadOnlyCollection<Price> ExchangeRates => _exchangeRates;

    public void AddExchangeRate(Price price)
    {
        _exchangeRates.Add(price);
    }

    public bool IsEmpty() => string.IsNullOrWhiteSpace(Name);
}