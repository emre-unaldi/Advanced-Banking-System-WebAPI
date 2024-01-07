namespace Application.Features.Accounts.Commands.Update;

public class UpdateAccountResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public string Password { set; get; }
    public double Balance { get; set; }
    public string Bank { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}