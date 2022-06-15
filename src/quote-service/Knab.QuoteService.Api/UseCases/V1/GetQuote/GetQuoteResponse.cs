namespace Knab.QuoteService.Api.UseCases.V1.GetQuote;

public sealed class GetQuoteResponse
{
    public GetQuoteResponse(string name, string symbol, decimal price, string currency)
    {
        Name = name;
        Symbol = symbol.ToUpper();
        Price = price;
        Currency = currency;
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        ExchangeRates = new List<ExchangeRateModel>();
    }
    
    public string Name { get; }
    public string Symbol { get; }
    public decimal Price { get; }
    public string Currency { get; }
    public string Date { get; }
    public ICollection<ExchangeRateModel> ExchangeRates { get; }
}

public class ExchangeRateModel
{
    public ExchangeRateModel(decimal rate, string currency)
    {
        Rate = rate;
        Currency = currency;
    }

    public decimal Rate { get; }
    public string Currency { get; }
}