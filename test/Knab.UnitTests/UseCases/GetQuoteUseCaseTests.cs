using FluentAssertions;
using Knab.QuoteService.Api.UseCases.V1.GetQuote;
using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Application.UseCases.GetQuote;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Knab.UnitTests.UseCases;

public class GetQuoteUseCaseTests
{
    private readonly Mock<ICryptoCurrencyService> _cryptoCurrencyServiceMock;
    private readonly Mock<ICurrencyExchangeService> _currencyExchangeServiceMock;
    private readonly GetQuoteUseCase _sut;

    public GetQuoteUseCaseTests()
    {
        _cryptoCurrencyServiceMock = new Mock<ICryptoCurrencyService>();
        _currencyExchangeServiceMock = new Mock<ICurrencyExchangeService>();
        _sut = new GetQuoteUseCase(_cryptoCurrencyServiceMock.Object, _currencyExchangeServiceMock.Object, new GetQuoteValidator());
    }
    
    [Fact]
    public async Task Should_Return_Ok_Given_Valid_Input()
    {
        const string expectedName = "Bitcoin";
        var expectedSymbol = Symbol.Bitcoin;
        var expectedPrice = new Price(123, Currency.Usd);
        var expectedRates = new List<ExchangeRateModel>
        {
            new(rate: 1, Currency.Eur.Code)
        };
        _cryptoCurrencyServiceMock
            .Setup(x => x.GetCryptoCurrencyAsync(It.IsAny<Symbol>()))
            .ReturnsAsync(new Quote(expectedName, expectedSymbol, expectedPrice));
        _currencyExchangeServiceMock
            .Setup(x => x.GetExchangeRateAsync(It.IsAny<Price>(), It.IsAny<ICollection<Currency>>()))
            .ReturnsAsync(new List<Price>{ new(Amount: 1, Currency.Eur) });
        var presenter = new GetQuotePresenter();
        
        await _sut.Execute(Symbol.Bitcoin, presenter);
        
        var actualResult = presenter.ViewModel.Should().BeOfType<OkObjectResult>();
        var actualResponse = actualResult.Subject.Value.Should().BeOfType<GetQuoteResponse>().Subject;
        actualResponse.Name.Should().Be(expectedName);
        actualResponse.Symbol.Should().Be(expectedSymbol.Code);
        actualResponse.Price.Should().Be(expectedPrice.Amount);
        actualResponse.Currency.Should().Be(expectedPrice.Currency.Code);
        actualResponse.Date.Should().Be(DateTime.Now.ToString("yyyy-MM-dd"));
        actualResponse.ExchangeRates.Should().NotBeNull().And.HaveCountGreaterThan(0).And.Contain(expectedRates);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Given_InValid_Input()
    {
        var presenter = new GetQuotePresenter();
        
        await _sut.Execute(new Symbol("Invalid_Input"), presenter);

        presenter.ViewModel.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_NotFound_When_CryptoCurrency_Not_Existed()
    {
        _cryptoCurrencyServiceMock
            .Setup(x => x.GetCryptoCurrencyAsync(It.IsAny<Symbol>()))
            .ReturnsAsync(Quote.None);
        var presenter = new GetQuotePresenter();
        
        await _sut.Execute(new Symbol("ZZZZ"), presenter);

        presenter.ViewModel.Should().BeOfType<NotFoundResult>();
    }
}