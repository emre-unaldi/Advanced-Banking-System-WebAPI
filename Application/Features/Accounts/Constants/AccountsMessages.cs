using Core.Application.Rules;

namespace Application.Features.Accounts.Constants;

public class AccountsMessages : BaseBusinessRules
{
    public const string AccountNumberExists = "Account number exists";
    public const string AccountNotFound = "Account not found";
    public const string TargetAccountNotFound = "Target account not found";
    public const string AccountAssociatedUserNotFound = "Account associated user not found";
    public const string AccountBalanceIsNotEnough = "Account balance is not enough";
    public const string AccountTypeCannotBeEmpty = "Account type cannot be empty";
    public const string AccountTypeMustBeBetweenMinZeroAndMaxThree = "Account type must take the values 0, 1, 2, or 3";
    public const string AccountPasswordCannotBeEmpty = "Account password cannot be empty";
    public const string AccountPasswordMustBeAtLeastEightCharacters = "Account password must be at least 8 characters";
    public const string AccountPasswordMustContainLowercaseLettersUppercaseLettersAndNumbers = "Account password must contain lowercase letters, uppercase letters and numbers";
    public const string AccountBalanceCannotBeEmpty = "Account balance cannot be empty";
    public const string AccountBalanceCannotBeANegativeValue = "Account balance cannot be a negative value";
    public const string AccountBankNameCannotBeEmpty = "Account bank name cannot be empty";
    public const string AccountBankNameMustBeBetweenMinAndMaxCharacters = "Account bank name must be minimum 3 and maximum 30 characters";
    public const string AccountUserIdCannotBeEmpty = "Account user id cannot be empty";
    public const string AccountUserIdMustBeGreaterThanZero = "Account user id must be greater than 0";
    public const string AccountIdCannotBeEmpty = "Account id cannot be empty";
    public const string AccountIdMustBeGreaterThanZero = "Account id must be greater than 0";
    public const string AccountPageIndexMustBeGreaterThanOrEqualToZero = "Account page number must be greater than or equal to 0";
    public const string AccountPageSizeMustBeGreaterThanOrEqualToZero = "Account page size must be greater than or equal to 0";
}

