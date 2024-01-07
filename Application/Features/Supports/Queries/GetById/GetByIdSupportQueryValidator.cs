using Application.Features.Supports.Constants;
using FluentValidation;

namespace Application.Features.Supports.Queries.GetById;

public class GetByIdSupportQueryValidator : AbstractValidator<GetByIdSupportQuery>
{
    public GetByIdSupportQueryValidator()
    {
        RuleFor(support => support.Id)
            .NotEmpty().WithMessage(SupportsMessages.SupportIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(SupportsMessages.SupportIdMustBeGreaterThanZero);
    }
}
