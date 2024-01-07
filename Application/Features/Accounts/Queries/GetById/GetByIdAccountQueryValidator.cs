using Application.Features.Accounts.Constants;
using FluentValidation;

namespace Application.Features.Accounts.Queries.GetById;

public class GetByIdAccountQueryValidator : AbstractValidator<GetByIdAccountQuery>
{
    public GetByIdAccountQueryValidator()
    {
        RuleFor(account => account.Id)
            .NotEmpty().WithMessage(AccountsMessages.AccountIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(AccountsMessages.AccountIdMustBeGreaterThanZero);
    }
}
