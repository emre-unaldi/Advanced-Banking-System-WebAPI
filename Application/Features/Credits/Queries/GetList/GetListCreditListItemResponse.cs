namespace Application.Features.Credits.Queries.GetList;

public class GetListCreditListItemResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double RequestedLoanAmount { get; set; }
    public double TotalPaymentAmount { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public string ReferredBank { get; set; }
    public short MonthlyPaymentDate { get; set; }
    public bool ApprovalStatus { get; set; }
    public GetListCreditUserResponseDto User { get; set; }
    public DateTime CreatedDate { get; set; }
}
