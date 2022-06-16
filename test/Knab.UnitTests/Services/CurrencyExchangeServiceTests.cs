using FluentAssertions;
using Knab.QuoteService.Domain.ValueObjects;
using Knab.QuoteService.Infrastructure.CurrencyExchange;
using Xunit;

namespace Knab.UnitTests.Services;

public class CurrencyExchangeServiceTests
{
    [Fact]
    public async Task Should_Convert_Price_To_Given_Rates()
    {
        var (amount, _) = new Money(5, Currency.Usd);
        var expectedPrices = new List<Money>
        {
            new((decimal)1.457322 * amount, Currency.Aud),
            new((decimal)5.139496 * amount, Currency.Brl),
            new((decimal)0.96034 * amount, Currency.Eur),
            new((decimal)0.83501 * amount, Currency.Gbp),
            new(1 * amount, Currency.Usd),
        };
        var sut = new CurrencyExchangeServiceFake();

        var actualPrices = await sut.GetExchangeRateAsync(new Money(5, Currency.Usd),
            new[]
            {
                Currency.Aud,
                Currency.Eur,
                Currency.Gbp,
                Currency.Brl,
                Currency.Usd
            });

        actualPrices.Should().NotBeNullOrEmpty().And.Contain(expectedPrices);
    }
}