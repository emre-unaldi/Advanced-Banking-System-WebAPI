﻿using Application.Features.Users.Constants;
using FluentValidation;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage(UsersMessages.UserFirstNameCannotBeEmpty);

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage(UsersMessages.UserLastNameCannotBeEmpty);

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(UsersMessages.UserEmailCannotBeEmpty)
            .EmailAddress().WithMessage(UsersMessages.UserNotAValidEmailAddress);

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage(UsersMessages.UserPasswordCannotBeEmpty)
            .MinimumLength(8).WithMessage(UsersMessages.UserPasswordMustBeAtLeastEightharacters);

        RuleFor(user => user.IdentityNumber)
            .NotEmpty().WithMessage(UsersMessages.UserIdentityNumberCannotBeEmpty);

        RuleFor(user => user.PhoneNumber)
            .NotEmpty().WithMessage(UsersMessages.UserPhoneNumberCannotBeEmpty);

        RuleFor(user => user.Address)
            .NotEmpty().WithMessage(UsersMessages.UserAddressCannotBeEmpty);

        RuleFor(user => user.FindexScore)
            .NotEmpty().WithMessage(UsersMessages.UserFindexScoreCannotBeEmpty)
            .GreaterThanOrEqualTo(0).WithMessage(UsersMessages.UserFindexScoreMustBeGreaterThanOrEqualToZero);
    }
}
