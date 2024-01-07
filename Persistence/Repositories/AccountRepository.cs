using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AccountRepository : EfRepositoryBase<Account, int, BaseDbContext>, IAccountRepository
{
    public AccountRepository(BaseDbContext context) : base(context)
    {
    }
}
