using Application.Features.Credits.Constants;
using FluentValidation;

namespace Application.Features.Credits.Commands.Update;

public class UpdateCreditCommandValidator : AbstractValidator<UpdateCreditCommand>
{
    public UpdateCreditCommandValidator()
    {
        RuleFor(credit => credit.Id)
            .NotEmpty().WithMessage(CreditsMessages.CreditIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(CreditsMessages.CreditIdMustBeGreaterThanZero);

        RuleFor(credit => credit.Name)
           .NotEmpty().WithMessage(CreditsMessages.CreditNameCannotBeEmpty)
           .MinimumLength(5).WithMessage(CreditsMessages.CreditNameMustBeAMinimumOfFiveCharacters);

        RuleFor(credit => credit.RequestedLoanAmount)
            .NotEmpty().WithMessage(CreditsMessages.CreditRequestedLoanAmountCannotBeEmpty);

        RuleFor(credit => credit.TotalPaymentAmount)
            .NotEmpty().WithMessage(CreditsMessages.CreditTotalPaymentAmountCannotBeEmpty);

        RuleFor(credit => credit.MonthlyPaymentAmount)
            .NotEmpty().WithMessage(CreditsMessages.CreditMonthlyPaymentAmountCannotBeEmpty);

        RuleFor(credit => credit.MonthlyPaymentDate)
            .NotEmpty().WithMessage(CreditsMessages.CreditMonthlyPaymentDateCannotBeEmpty)
            .Must(date => date >= 1 && date <= 31).WithMessage(CreditsMessages.CreditMonthlyPaymentDateMustBeInTheRangeOfTwoNumbers);

        RuleFor(credit => credit.ReferredBank)
            .NotEmpty().WithMessage(CreditsMessages.CreditReferredBankNameCannotBeEmpty)
            .MinimumLength(3).WithMessage(CreditsMessages.CreditReferredBankNameMustBeAMinimumOfFiveCharacters);

        RuleFor(credit => credit.UserId)
            .NotEmpty().WithMessage(CreditsMessages.CreditUserIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(CreditsMessages.CreditUserIdMustBeGreaterThanZero);
    }
}
