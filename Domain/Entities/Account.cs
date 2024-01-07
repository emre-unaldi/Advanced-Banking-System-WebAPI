using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Account : Entity<int>
{
    public string AccountNumber { get; set; }
    public AccountType AccountType { get; set; }
    public string Password { set; get; }
    public double Balance { get; set; }
    public string Bank { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
}

