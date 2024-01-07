using Application.Features.Accounts.Constants;
using FluentValidation;

namespace Application.Features.Accounts.Commands.Delete;

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(account => account.Id)
            .NotEmpty().WithMessage(AccountsMessages.AccountIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(AccountsMessages.AccountIdMustBeGreaterThanZero);
    }
}
