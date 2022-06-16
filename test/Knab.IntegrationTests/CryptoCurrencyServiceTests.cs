using FluentAssertions;
using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using Knab.QuoteService.Infrastructure.CryptoCurrency;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Knab.IntegrationTests;

public class CryptoCurrencyServiceTests : IClassFixture<ServiceCollectionFixture>
{
    private readonly ServiceCollection _services;

    public CryptoCurrencyServiceTests(ServiceCollectionFixture services)
    {
        _services = services.Services;
    }

    [Fact]
    public async Task Should_Return_CryptoCurrency()
    {
        _services.AddHttpClient(CryptoCurrencyService.HttpClientName, client =>
        {
            client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/v2/cryptocurrency/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "eea8cae1-bfac-4a93-8431-faf9b44c933d");
        });
        _services.AddSingleton<ICryptoCurrencyService, CryptoCurrencyService>();
        var serviceProvider = _services.BuildServiceProvider();
        var sut = serviceProvider.GetRequiredService<ICryptoCurrencyService>();

        var actualResult = await sut.GetCryptoCurrencyAsync(Symbol.Bitcoin);

        actualResult.Name.Should().Be("Bitcoin");
        actualResult.Symbol.Should().Be(Symbol.Bitcoin);
        actualResult.Money.Should().NotBe(0);
    }


}