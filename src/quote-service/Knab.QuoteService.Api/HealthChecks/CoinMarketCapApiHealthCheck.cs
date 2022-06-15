using Knab.QuoteService.Application.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Knab.QuoteService.Api.HealthChecks;

public class CoinMarketCapApiHealthCheck : IHealthCheck
{
    private readonly ICryptoCurrencyService _cryptoCurrencyService;

    public CoinMarketCapApiHealthCheck(ICryptoCurrencyService cryptoCurrencyService)
    {
        _cryptoCurrencyService = cryptoCurrencyService;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            await _cryptoCurrencyService.GetCryptoCurrencyAsync("BTC");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"{nameof(CoinMarketCapApiHealthCheck)}: Exception during check: {ex.GetType().FullName}", ex);
        }
        
        return HealthCheckResult.Healthy($"{nameof(CoinMarketCapApiHealthCheck)}: Healthy");
    }
}