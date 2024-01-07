using Application.Features.Users.Constants;
using FluentValidation;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(support => support.Id)
            .NotEmpty().WithMessage(UsersMessages.UserIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(UsersMessages.UserIdMustBeGreaterThanZero);
    }
}
