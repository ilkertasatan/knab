namespace Knab.QuoteService.Domain.ValueObjects;

public record Symbol(string Code)
{
    public static readonly Symbol Bitcoin = new("BTC");

    public override string ToString() => Code.ToUpper();
}