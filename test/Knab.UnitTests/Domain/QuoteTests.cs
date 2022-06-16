using FluentAssertions;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using Xunit;

namespace Knab.UnitTests.Domain;

public class QuoteTests
{
    [Fact]
    public void Should_Return_IsEmpty_True()
    {
        var sut = Quote.None;

        sut.IsEmpty().Should().BeTrue();
    }

    [Fact]
    public void Should_Return_IsEmpty_False()
    {
        var sut = new Quote("Bitcoin", Symbol.Bitcoin, new Money(Amount: 1, Currency.Usd));

        sut.IsEmpty().Should().BeFalse();
    }

    [Fact]
    public void Should_AddExchangeRate()
    {
        var sut = new Quote("Bitcoin", Symbol.Bitcoin, new Money(Amount: 1, Currency.Usd));

        sut.AddExchangeRate(new Money(Amount: 1, Currency.Eur));

        sut.ExchangeRates.Should().NotBeEmpty().And.HaveCountGreaterThan(0);
    }
}