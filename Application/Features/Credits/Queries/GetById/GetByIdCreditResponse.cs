namespace Application.Features.Credits.Queries.GetById;

public class GetByIdCreditResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double RequestedLoanAmount { get; set; }
    public double TotalPaymentAmount { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public string ReferredBank { get; set; }
    public short MonthlyPaymentDate { get; set; }
    public bool ApprovalStatus { get; set; }
    public GetByIdCreditUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
