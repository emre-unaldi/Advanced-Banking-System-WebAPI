﻿using Application.Features.Activities.Constants;
using FluentValidation;

namespace Application.Features.Activities.Commands.WithdrawMoney;

public class WithdrawMoneyActivitiesCommandValidator : AbstractValidator<WithdrawMoneyActivitiesCommand>
{
    public WithdrawMoneyActivitiesCommandValidator()
    {
        RuleFor(activity => activity.AccountNumber)
            .NotEmpty().WithMessage(ActivitiesMessages.ActivityAccountNumberCannotBeEmpty);

        RuleFor(activity => activity.Amount)
            .NotEmpty().WithMessage(ActivitiesMessages.ActivityAmountCannotBeEmpty)
            .GreaterThan(0).WithMessage(ActivitiesMessages.ActivityAmountMustBeAPositiveNumber);

        RuleFor(activity => activity.UserId)
            .NotEmpty().WithMessage(ActivitiesMessages.ActivityUserIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(ActivitiesMessages.ActivityUserIdMustBeGreaterThanZero);
    }
}
