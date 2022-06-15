using FluentValidation.Results;

namespace Knab.QuoteService.Application.Extensions;

public static class ValidationFailureExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> validationFailures)
    {
        return validationFailures.ToDictionary(
            validationFailure => validationFailure.PropertyName,
            validationFailure => new[] {validationFailure.ErrorMessage});
    }
}