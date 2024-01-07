namespace Application.Features.Accounts.Queries.GetList;

public class GetListAccountListItemResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public string Password { set; get; }
    public double Balance { get; set; }
    public string Bank { get; set; }
    public GetListAccountUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
}
