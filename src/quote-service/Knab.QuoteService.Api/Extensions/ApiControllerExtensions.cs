using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;

namespace Knab.QuoteService.Api.Extensions;

public static class ApiControllerExtensions
{
    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(config =>
            {
                config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });
        
        services.AddEndpointsApiExplorer();
        return services;
    }
}