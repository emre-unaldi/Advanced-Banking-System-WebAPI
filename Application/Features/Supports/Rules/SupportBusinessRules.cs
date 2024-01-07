using Application.Features.Supports.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Supports.Rules;

public class SupportBusinessRules : BaseBusinessRules
{
    private readonly ISupportRepository _supportRepository;

    public SupportBusinessRules(ISupportRepository supportRepository)
    {
        _supportRepository = supportRepository;
    }

    public async Task SupportTitleCannotBeDuplicated(string title)
    {
        Support? support = await _supportRepository.GetAsync(predicate: support => support.Title.ToLower() == title.ToLower());

        if (support is not null)
            throw new BusinessException(SupportsMessages.SupportTitleExists);
    }

    public async Task SupportMustBePresent(int id)
    {
        Support? support = await _supportRepository.GetAsync(predicate: support => support.Id == id);

        if (support is null)
            throw new BusinessException(SupportsMessages.SupportNotFound);
    }
}
