using Application.Features.Users.Constants;
using FluentValidation;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQueryValidator : AbstractValidator<GetByIdUserQuery>
{
    public GetByIdUserQueryValidator()
    {
        RuleFor(support => support.Id)
            .NotEmpty().WithMessage(UsersMessages.UserIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(UsersMessages.UserIdMustBeGreaterThanZero);
    }
}
