using Application.Features.Credits.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Credits.Rules;

public class CreditBusinessRules : BaseBusinessRules
{
    private readonly ICreditRepository _creditRepository;

    public CreditBusinessRules(ICreditRepository creditRepository)
    {
        _creditRepository = creditRepository;
    }

    public async Task CreditMustBePresent(int id)
    {
        Credit? credit = await _creditRepository.GetAsync(predicate: credit => credit.Id == id);

        if (credit is null)
            throw new BusinessException(CreditsMessages.CreditNotFound);
    }
}
