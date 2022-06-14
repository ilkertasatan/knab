namespace Knab.QuoteService.Api.Extensions;

public static class ApiControllerExtensions
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        return services;
    }
}