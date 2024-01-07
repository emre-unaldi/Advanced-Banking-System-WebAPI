using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Credit : Entity<int>
{
    public string Name { get; set; }    
    public double RequestedLoanAmount { get; set; }
    public double TotalPaymentAmount { get; set; }
    public double MonthlyPaymentAmount { get; set; }
    public string ReferredBank { get; set; }
    public short MonthlyPaymentDate { get; set; }
    public bool ApprovalStatus { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }  
}
