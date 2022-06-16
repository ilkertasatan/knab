using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Domain;

public class Quote
{
    public static readonly Quote None = new();
    
    private readonly List<Money> _exchangeRates;
    private Quote() { }

    public Quote(string name, Symbol symbol, Money money)
    {
        Name = name;
        Symbol = symbol;
        Money = money;
        _exchangeRates = new List<Money>();
    }

    public string Name { get; }

    public Symbol Symbol { get; }

    public Money Money { get; }

    public IReadOnlyCollection<Money> ExchangeRates => _exchangeRates;

    public void AddExchangeRate(Money money)
    {
        _exchangeRates.Add(money);
    }

    public bool IsEmpty() => string.IsNullOrWhiteSpace(Name);
}