namespace Knab.QuoteService.Domain.ValueObjects;

public record Money(decimal Amount, Currency Currency)
{
    public Money Convert(decimal rate, Currency targetCurrency)
    {
        return new Money(rate * Amount, targetCurrency);
    }
}