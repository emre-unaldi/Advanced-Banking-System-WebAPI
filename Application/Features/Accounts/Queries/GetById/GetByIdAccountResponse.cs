namespace Application.Features.Accounts.Queries.GetById;

public class GetByIdAccountResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public string Password { set; get; }
    public double Balance { get; set; }
    public string Bank { get; set; }
    public GetByIdAccountUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
