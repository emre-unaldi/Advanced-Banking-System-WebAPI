using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Support : Entity<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; }
}