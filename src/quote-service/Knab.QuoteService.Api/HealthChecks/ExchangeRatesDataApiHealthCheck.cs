using Knab.QuoteService.Application.Services;
using Knab.QuoteService.Domain.ValueObjects;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Knab.QuoteService.Api.HealthChecks;

public class ExchangeRatesDataApiHealthCheck : IHealthCheck
{
    private readonly ICurrencyExchangeService _currencyExchangeService;

    public ExchangeRatesDataApiHealthCheck(ICurrencyExchangeService currencyExchangeService)
    {
        _currencyExchangeService = currencyExchangeService;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            await _currencyExchangeService.GetExchangeRateAsync(new Price(Amount: 1, Currency.Usd), new[] {Currency.Gbp});
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy($"{nameof(ExchangeRatesDataApiHealthCheck)}: Exception during check: {ex.GetType().FullName}", ex);
        }
        
        return HealthCheckResult.Healthy($"{nameof(ExchangeRatesDataApiHealthCheck)}: Healthy");
    }
}