using FluentAssertions;
using Knab.QuoteService.Api.UseCases.V1.GetQuote;
using Knab.QuoteService.Domain;
using Knab.QuoteService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Knab.UnitTests.UseCases;

public class GetQuotePresenterTests
{
    [Fact]
    public void Should_Return_Ok()
    {
        var sut = new GetQuotePresenter();

        sut.Ok(new Quote("Bitcoin", Symbol.Bitcoin, new Money(Amount: 1, Currency.Usd)));

        sut.ViewModel.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public void Should_Return_BadRequest()
    {
        var sut = new GetQuotePresenter();

        sut.BadRequest(new Dictionary<string, string[]>());

        sut.ViewModel.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void Should_Return_NotFound()
    {
        var sut = new GetQuotePresenter();

        sut.NotFound();

        sut.ViewModel.Should().BeOfType<NotFoundResult>();
    }
}