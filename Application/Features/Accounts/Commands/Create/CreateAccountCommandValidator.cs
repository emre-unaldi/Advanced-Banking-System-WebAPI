using Application.Features.Accounts.Constants;
using FluentValidation;

namespace Application.Features.Accounts.Commands.Create;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(account => account.AccountType)
            .NotNull().WithMessage(AccountsMessages.AccountTypeCannotBeEmpty)
            .InclusiveBetween(0, 3).WithMessage(AccountsMessages.AccountTypeMustBeBetweenMinZeroAndMaxThree);

        RuleFor(account => account.Password)
            .NotEmpty().WithMessage(AccountsMessages.AccountPasswordCannotBeEmpty)
            .MinimumLength(8).WithMessage(AccountsMessages.AccountPasswordMustBeAtLeastEightCharacters)
            .Matches("[A-Za-z0-9]").WithMessage(AccountsMessages.AccountPasswordMustContainLowercaseLettersUppercaseLettersAndNumbers);

        RuleFor(account => account.Balance)
            .NotEmpty().WithMessage(AccountsMessages.AccountBalanceCannotBeEmpty)
            .GreaterThanOrEqualTo(0).WithMessage(AccountsMessages.AccountBalanceCannotBeANegativeValue);

        RuleFor(account => account.Bank)
            .NotEmpty().WithMessage(AccountsMessages.AccountBankNameCannotBeEmpty)
            .Length(3, 30).WithMessage(AccountsMessages.AccountBankNameMustBeBetweenMinAndMaxCharacters);

        RuleFor(account => account.UserId)
            .NotEmpty().WithMessage(AccountsMessages.AccountUserIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(AccountsMessages.AccountUserIdMustBeGreaterThanZero);
    }
}
