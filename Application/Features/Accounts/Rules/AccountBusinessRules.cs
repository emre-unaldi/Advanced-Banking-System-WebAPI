using Application.Features.Accounts.Constants;
using Application.Features.Supports.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Accounts.Rules;

public class AccountBusinessRules : BaseBusinessRules
{
    private readonly IAccountRepository _accountRepository;

    public AccountBusinessRules(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task AccountNumberCannotBeDuplicated(string accountNumber)
    {
        Account? account = await _accountRepository.GetAsync(predicate: account => account.AccountNumber == accountNumber);

        if (account is not null)
            throw new BusinessException(AccountsMessages.AccountNumberExists);
    }

    public async Task AccountMustBePresent(int id)
    {
        Account? account = await _accountRepository.GetAsync(predicate: account => account.Id == id);

        if (account is null)
            throw new BusinessException(AccountsMessages.AccountNotFound);
    }
}
