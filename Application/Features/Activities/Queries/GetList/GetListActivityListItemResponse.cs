namespace Application.Features.Activities.Queries.GetList;

public class GetListActivityListItemResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public double Amount { get; set; }
    public string? TargetAccountNumber { get; set; }
    public string TransactionType { get; set; }
    public DateTime TransactionDate { get; }
    public virtual GetListActivityUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
}
