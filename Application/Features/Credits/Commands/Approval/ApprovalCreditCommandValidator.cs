using Application.Features.Credits.Constants;
using FluentValidation;

namespace Application.Features.Credits.Commands.Approval;

public class ApprovalCreditCommandValidator : AbstractValidator<ApprovalCreditCommand>
{
    public ApprovalCreditCommandValidator()
    {
        RuleFor(credit => credit.Id)
            .NotEmpty().WithMessage(CreditsMessages.CreditIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(CreditsMessages.CreditIdMustBeGreaterThanZero);
    }
}
