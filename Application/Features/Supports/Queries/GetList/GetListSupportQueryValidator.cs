using Application.Features.Supports.Constants;
using FluentValidation;

namespace Application.Features.Supports.Queries.GetList;

public class GetListSupportQueryValidator : AbstractValidator<GetListSupportQuery>
{
    public GetListSupportQueryValidator()
    {
        RuleFor(support => support.PageRequest.PageIndex)
            .Must(pageSize => pageSize >= 0).WithMessage(SupportsMessages.SupportPageIndexMustBeGreaterThanOrEqualToZero);

        RuleFor(support => support.PageRequest.PageSize)
            .Must(pageSize => pageSize >= 0).WithMessage(SupportsMessages.SupportPageSizeMustBeGreaterThanOrEqualToZero);
    }
}
