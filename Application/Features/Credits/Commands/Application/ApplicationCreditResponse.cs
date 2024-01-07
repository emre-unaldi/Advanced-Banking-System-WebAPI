namespace Application.Features.Credits.Commands.Application;

public class ApplicationCreditResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double RequestedLoanAmount { get; set; }
    public double TotalPaymentAmount { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public string ReferredBank { get; set; }
    public short MonthlyPaymentDate { get; set; }
    public bool ApprovalStatus { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}