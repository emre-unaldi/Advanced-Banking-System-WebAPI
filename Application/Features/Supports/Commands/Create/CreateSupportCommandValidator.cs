using Application.Features.Supports.Constants;
using FluentValidation;

namespace Application.Features.Supports.Commands.Create;

public class CreateSupportCommandValidator : AbstractValidator<CreateSupportCommand>
{
    public CreateSupportCommandValidator()
    {
        RuleFor(support => support.Title)
            .NotEmpty().WithMessage(SupportsMessages.SupportTitleCannotBeEmpty)
            .Length(4, 20).WithMessage(SupportsMessages.SupportTitleMustBeBetweenMinAndMaxCharacters);

        RuleFor(support => support.Content)
            .NotEmpty().WithMessage(SupportsMessages.SupportContentCannotBeEmpty)
            .Length(20, 200).WithMessage(SupportsMessages.SupportContentMustBeBetweenMinAndMaxCharacters);

        RuleFor(support => support.UserId)
            .GreaterThan(0).WithMessage(SupportsMessages.SupportUserIdMustBeGreaterThanZero);
    }
}
