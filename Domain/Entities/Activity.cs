using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Activity : Entity<int>
{
    public string AccountNumber { get; set; }
    public double Amount { get; set; }
    public string? TargetAccountNumber { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime TransactionDate { get; } = DateTime.UtcNow;

    public int UserId { get; set; }
    public virtual User User { get; set; }
}