using Application.Features.Supports.Constants;
using FluentValidation;

namespace Application.Features.Supports.Commands.Delete;

public class DeleteSupportCommandValidator : AbstractValidator<DeleteSupportCommand>
{
    public DeleteSupportCommandValidator()
    {
        RuleFor(support => support.Id)
            .NotEmpty().WithMessage(SupportsMessages.SupportIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(SupportsMessages.SupportIdMustBeGreaterThanZero);
    }
}