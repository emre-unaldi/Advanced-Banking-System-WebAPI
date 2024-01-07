using Application.Features.Credits.Constants;
using FluentValidation;

namespace Application.Features.Credits.Queries.GetList;

public class GetListCreditQueryValidator : AbstractValidator<GetListCreditQuery>
{
    public GetListCreditQueryValidator()
    {
        RuleFor(credit => credit.PageRequest.PageIndex)
            .Must(pageSize => pageSize >= 0).WithMessage(CreditsMessages.CreditPageIndexMustBeGreaterThanOrEqualToZero);

        RuleFor(credit => credit.PageRequest.PageSize)
            .Must(pageSize => pageSize >= 0).WithMessage(CreditsMessages.CreditPageSizeMustBeGreaterThanOrEqualToZero);
    }
}
