using Application.Features.Accounts.Constants;
using FluentValidation;

namespace Application.Features.Activities.Queries.GetList;

public class GetListActivityQueryValidator : AbstractValidator<GetListActivityQuery>
{
    public GetListActivityQueryValidator()
    {
        RuleFor(account => account.PageRequest.PageIndex)
            .Must(pageSize => pageSize >= 0).WithMessage(AccountsMessages.AccountPageIndexMustBeGreaterThanOrEqualToZero);

        RuleFor(account => account.PageRequest.PageSize)
            .Must(pageSize => pageSize >= 0).WithMessage(AccountsMessages.AccountPageSizeMustBeGreaterThanOrEqualToZero);
    }
}
