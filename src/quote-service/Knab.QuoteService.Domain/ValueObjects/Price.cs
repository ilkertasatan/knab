namespace Knab.QuoteService.Domain.ValueObjects;

public record Price(decimal Amount, Currency Currency)
{
    public Price Convert(decimal rate, Currency targetCurrency)
    {
        return new Price(rate * Amount, targetCurrency);
    }
}