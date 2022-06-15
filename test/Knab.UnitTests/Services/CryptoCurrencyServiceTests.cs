using FluentAssertions;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using Knab.QuoteService.Infrastructure.CryptoCurrency;
using Xunit;

namespace Knab.UnitTests.Services;

public class CryptoCurrencyServiceTests
{
    [Fact]
    public async Task Should_Return_CryptoCurrency()
    {
        var expectedQuote = new Quote("Bitcoin", Symbol.Bitcoin, new Price(Amount: 21005, Currency.Usd));
        var sut = new CryptoCurrencyServiceFake();

        var actualCryptoCurrency = await sut.GetCryptoCurrencyAsync(Symbol.Bitcoin);

        actualCryptoCurrency.Should().BeEquivalentTo(expectedQuote);
    }
}