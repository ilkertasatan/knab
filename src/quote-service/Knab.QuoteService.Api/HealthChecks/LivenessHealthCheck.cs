using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Knab.QuoteService.Api.HealthChecks;

public class LivenessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = new()) => Task.FromResult(HealthCheckResult.Healthy());
}