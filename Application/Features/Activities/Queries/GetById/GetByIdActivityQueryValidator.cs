using Application.Features.Activities.Constants;
using FluentValidation;

namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityQueryValidator : AbstractValidator<GetByIdActivityQuery>
{
    public GetByIdActivityQueryValidator()
    {
        RuleFor(account => account.Id)
            .NotEmpty().WithMessage(ActivitiesMessages.ActivityIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(ActivitiesMessages.ActivityIdMustBeGreaterThanZero);
    }
}
