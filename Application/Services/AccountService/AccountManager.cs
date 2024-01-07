using Application.Features.Accounts.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Services.AccountService;

public class AccountManager : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountManager(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task DepositMoney(string accountNumber, double amount)
    {
        Account? account = await _accountRepository.GetAsync(predicate: account => account.AccountNumber == accountNumber);

        if (account is not null)
            account.Balance += amount;
        else
            throw new BusinessException(AccountsMessages.AccountNotFound);

        await _accountRepository.UpdateAsync(account);
    }

    public async Task WithdrawMoney(string accountNumber, double amount)
    {
        Account? account = await _accountRepository.GetAsync(predicate: account => account.AccountNumber == accountNumber);

        if (account is not null)
        {
            if (account.Balance < amount)
                throw new BusinessException(AccountsMessages.AccountBalanceIsNotEnough);
            account.Balance -= amount;
        }
        else
            throw new BusinessException(AccountsMessages.AccountNotFound);

        await _accountRepository.UpdateAsync(account);
    }

    public async Task MoneyTransfer(string accountNumber, string targetAccountNumber, double amount)
    {
        await WithdrawMoney(accountNumber, amount);

        Account? targetAccount = await _accountRepository.GetAsync(predicate: account => account.AccountNumber == targetAccountNumber);

        if (targetAccount is not null)
            targetAccount.Balance += amount;
        else
            throw new BusinessException(AccountsMessages.TargetAccountNotFound);

        await _accountRepository.UpdateAsync(targetAccount);
    }
}
