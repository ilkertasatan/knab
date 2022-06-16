using FluentAssertions;
using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using Knab.QuoteService.Infrastructure.CurrencyExchange;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Knab.IntegrationTests;

public class CurrencyExchangeServiceTests : IClassFixture<ServiceCollectionFixture>
{
    private readonly ServiceCollection _services;

    public CurrencyExchangeServiceTests(ServiceCollectionFixture services)
    {
        _services = services.Services;
    }
    
    [Fact]
    public async Task Should_Convert_Price_To_Given_Rates()
    {
        _services.AddHttpClient(CurrencyExchangeService.HttpClientName, client =>
        {
            client.BaseAddress = new Uri("https://api.apilayer.com/exchangerates_data/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("apikey", "pY7WQVyxVF0ymF4n6o8XYRjzFW4pLvhc");
        });
        _services.AddSingleton<ICurrencyExchangeService, CurrencyExchangeService>();
        var serviceProvider = _services.BuildServiceProvider();
        var sut = serviceProvider.GetRequiredService<ICurrencyExchangeService>();

        var actualResult = await sut.GetExchangeRateAsync(new Price(1, Currency.Usd), new List<Currency> {Currency.Eur});

        actualResult.Should().NotBeEmpty().And.HaveCountGreaterThan(0);
    } 
}