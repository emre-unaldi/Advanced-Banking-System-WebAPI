namespace Application.Services.AccountService;

public interface IAccountService
{
    Task DepositMoney(string accountNumber, double amount);
    Task WithdrawMoney(string accountNumber, double amount);
    Task MoneyTransfer(string accountNumber, string targetAccountNumber, double amount);
}
