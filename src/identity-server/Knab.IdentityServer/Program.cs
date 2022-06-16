using Knab.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddIdentityServer()
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddDeveloperSigningCredential();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseIdentityServer();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Knab IdentityServer");
    });
});

app.Run();