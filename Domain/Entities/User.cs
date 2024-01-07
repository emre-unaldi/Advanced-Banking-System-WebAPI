using Core.Persistence.Repositories;

namespace Domain.Entities;

public class User : Entity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int FindexScore { get; set; }

    public virtual ICollection<Account> Accounts { get; set; }  
    public virtual ICollection<Credit> Credits { get; set; }  
}
