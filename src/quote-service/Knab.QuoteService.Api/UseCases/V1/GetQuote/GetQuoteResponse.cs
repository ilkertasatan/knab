namespace Knab.QuoteService.Api.UseCases.V1.GetQuote;

public sealed class GetQuoteResponse
{
    public GetQuoteResponse(string name, string symbol, QuoteModel quote)
    {
        Name = name;
        Symbol = symbol.ToUpper();
        Quote = quote;
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        ExchangeRates = new List<ExchangeRateModel>();
    }
    
    public string Name { get; }
    public string Symbol { get; }
    public QuoteModel Quote { get; }
    public string Date { get; }
    public ICollection<ExchangeRateModel> ExchangeRates { get; }
}

public sealed class QuoteModel
{
    public QuoteModel(decimal price, string currency)
    {
        Price = price;
        Currency = currency;
    }

    public decimal Price { get; }
    public string Currency { get; }
}

public sealed class ExchangeRateModel : IEquatable<ExchangeRateModel>
{
    public ExchangeRateModel(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    public bool Equals(ExchangeRateModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ExchangeRateModel) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }
}