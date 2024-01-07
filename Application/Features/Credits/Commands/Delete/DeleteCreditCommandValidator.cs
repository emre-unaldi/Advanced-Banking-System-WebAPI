using Application.Features.Credits.Constants;
using FluentValidation;

namespace Application.Features.Credits.Commands.Delete;

public class DeleteCreditCommandValidator : AbstractValidator<DeleteCreditCommand>
{
    public DeleteCreditCommandValidator()
    {
        RuleFor(credit => credit.Id)
            .NotEmpty().WithMessage(CreditsMessages.CreditIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(CreditsMessages.CreditIdMustBeGreaterThanZero);
    }
}
