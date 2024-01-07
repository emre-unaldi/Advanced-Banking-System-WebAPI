namespace Application.Features.Activities.Queries.GetById;

public class GetByIdActivityResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public double Amount { get; set; }
    public string? TargetAccountNumber { get; set; }
    public string TransactionType { get; set; }
    public DateTime TransactionDate { get; }
    public virtual GetByIdActivityUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
