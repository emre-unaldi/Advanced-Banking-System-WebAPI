using Application.Features.Users.Constants;
using FluentValidation;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQueryValidator : AbstractValidator<GetListUserQuery>
{
    public GetListUserQueryValidator()
    {
        RuleFor(user => user.PageRequest.PageIndex)
            .Must(pageSize => pageSize >= 0).WithMessage(UsersMessages.UserPageIndexMustBeGreaterThanOrEqualToZero);

        RuleFor(user => user.PageRequest.PageSize)
            .Must(pageSize => pageSize >= 0).WithMessage(UsersMessages.UserPageSizeMustBeGreaterThanOrEqualToZero);
    }
}
