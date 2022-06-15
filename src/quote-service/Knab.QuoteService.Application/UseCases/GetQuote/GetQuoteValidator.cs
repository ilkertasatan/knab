using FluentValidation;
using Knab.QuoteService.Domain.ValueObjects;

namespace Knab.QuoteService.Application.UseCases.GetQuote;

public sealed class GetQuoteValidator : AbstractValidator<Symbol>
{
    public GetQuoteValidator()
    {
        RuleFor(s => s.Code)
            .NotNull()
            .NotEmpty()
            .WithMessage("The 'Symbol Code' cannot be null or empty")
            .Length(1, 4)
            .WithMessage("The 'Symbol Code' must be between 1 and 4 characters");
    }
}