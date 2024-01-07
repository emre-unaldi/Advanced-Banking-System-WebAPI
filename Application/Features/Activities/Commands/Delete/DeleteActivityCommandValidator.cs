using Application.Features.Activities.Constants;
using FluentValidation;

namespace Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator()
    {
        RuleFor(account => account.Id)
            .NotEmpty().WithMessage(ActivitiesMessages.ActivityIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(ActivitiesMessages.ActivityIdMustBeGreaterThanZero);
    }
}
