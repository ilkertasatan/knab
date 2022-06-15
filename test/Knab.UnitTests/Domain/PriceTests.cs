using FluentAssertions;
using Knab.QuoteService.Domain.ValueObjects;
using Xunit;

namespace Knab.UnitTests.Domain;

public class PriceTests
{
    [Fact]
    public void Should_Convert_BasePrice_To_TargetPrice()
    {
        var expectedPrice = new Price(Amount: 20, Currency.Eur);
        var sut = new Price(Amount: 10, Currency.Usd);

        var actualPrice = sut.Convert(rate: 2, Currency.Eur);

        actualPrice.Should().Be(expectedPrice);
    }
}