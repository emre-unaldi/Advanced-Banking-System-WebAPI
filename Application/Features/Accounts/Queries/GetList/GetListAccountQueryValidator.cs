using Application.Features.Accounts.Constants;
using FluentValidation;

namespace Application.Features.Accounts.Queries.GetList;

public class GetListAccountQueryValidator : AbstractValidator<GetListAccountQuery>
{
    public GetListAccountQueryValidator()
    {
        RuleFor(account => account.PageRequest.PageIndex)
            .Must(pageSize => pageSize >= 0).WithMessage(AccountsMessages.AccountPageIndexMustBeGreaterThanOrEqualToZero);

        RuleFor(account => account.PageRequest.PageSize)
            .Must(pageSize => pageSize >= 0).WithMessage(AccountsMessages.AccountPageSizeMustBeGreaterThanOrEqualToZero);
    }
}
