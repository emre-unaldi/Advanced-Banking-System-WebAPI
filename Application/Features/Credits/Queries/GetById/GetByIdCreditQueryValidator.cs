using Application.Features.Credits.Constants;
using FluentValidation;

namespace Application.Features.Credits.Queries.GetById;

public class GetByIdCreditQueryValidator : AbstractValidator<GetByIdCreditQuery>
{
    public GetByIdCreditQueryValidator()
    {
        RuleFor(credit => credit.Id)
            .NotEmpty().WithMessage(CreditsMessages.CreditIdCannotBeEmpty)
            .GreaterThan(0).WithMessage(CreditsMessages.CreditIdMustBeGreaterThanZero);
    }
}
