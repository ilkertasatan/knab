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

public class QuotesControllerTests
{
    private readonly Mock<ICryptoCurrencyService> _cryptoCurrencyServiceMock;
    private readonly Mock<ICurrencyExchangeService> _currencyExchangeServiceMock;
    private readonly GetQuoteUseCase _useCase;
    private readonly GetQuotePresenter _presenter;

    public QuotesControllerTests()
    {
        _cryptoCurrencyServiceMock = new Mock<ICryptoCurrencyService>();
        _currencyExchangeServiceMock = new Mock<ICurrencyExchangeService>();
        _useCase = new GetQuoteUseCase(_cryptoCurrencyServiceMock.Object, _currencyExchangeServiceMock.Object, new GetQuoteValidator());
        _presenter = new GetQuotePresenter();
    }
    
    [Fact]
    public async Task Should_Return_Ok()
    {
        _cryptoCurrencyServiceMock
            .Setup(x => x.GetCryptoCurrencyAsync(It.IsAny<Symbol>()))
            .ReturnsAsync(new Quote("Bitcoin", Symbol.Bitcoin, new Money(Amount: 1, Currency.Usd)));
        _currencyExchangeServiceMock
            .Setup(x => x.GetExchangeRateAsync(It.IsAny<Money>(), It.IsAny<ICollection<Currency>>()))
            .ReturnsAsync(new List<Money>{ new(Amount: 1, Currency.Eur) });
        var sut = new QuotesController(_useCase, _presenter);

        await sut.GetQuotesAsync(new GetQuoteRequest
        {
            Symbol = Symbol.Bitcoin.Code
        });

        _presenter.ViewModel.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest()
    {
        var sut = new QuotesController(_useCase, _presenter);

        await sut.GetQuotesAsync(new GetQuoteRequest
        {
            Symbol = "ZZZZZ"
        });

        _presenter.ViewModel.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_NotFound()
    {
        _cryptoCurrencyServiceMock
            .Setup(x => x.GetCryptoCurrencyAsync(It.IsAny<Symbol>()))
            .ReturnsAsync(Quote.None);
        var sut = new QuotesController(_useCase, _presenter);

        await sut.GetQuotesAsync(new GetQuoteRequest
        {
            Symbol = "Invalid_Input"
        });

        _presenter.ViewModel.Should().BeOfType<BadRequestObjectResult>(); 
    }
}