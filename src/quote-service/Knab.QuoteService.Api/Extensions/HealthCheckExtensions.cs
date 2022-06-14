using Knab.QuoteService.Api.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Knab.QuoteService.Api.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<LivenessHealthCheck>("Liveness", HealthStatus.Unhealthy);
        services.AddHealthChecks().AddCheck<ExchangeRateApiHealthCheck>("Readiness", HealthStatus.Unhealthy);
            
        return services;
    }
}