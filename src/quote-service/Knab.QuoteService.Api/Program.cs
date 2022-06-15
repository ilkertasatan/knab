using HealthChecks.UI.Client;
using Knab.QuoteService.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiControllers()
    .AddSwagger()
    .AddHealthCheck()
    .AddMediatR()
    .AddUseCases(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthz/ready", new HealthCheckOptions
    {
        Predicate = check => !check.Name.Contains("Liveness"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapHealthChecks("/healthz/live", new HealthCheckOptions
    {
        Predicate = check => check.Name.Contains("Liveness"),
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    endpoints.MapControllers();
});

app.Run();