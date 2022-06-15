namespace Knab.QuoteService.Domain.ValueObjects;

public record Currency(string Code)
{
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Brl = new("BRL");
    public static readonly Currency Gbp = new("GBP");
    public static readonly Currency Aud = new("AUD");

    public override string ToString() => Code;
}