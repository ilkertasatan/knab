using FluentValidation.AspNetCore;
using Knab.QuoteService.Application.UseCases.GetQuote;

namespace Knab.QuoteService.Api.Extensions;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidation(configuration =>
        {
            configuration.RegisterValidatorsFromAssembly(typeof(IOutputPort).Assembly);
            configuration.AutomaticValidationEnabled = false;
        });

        return services;
    }
}