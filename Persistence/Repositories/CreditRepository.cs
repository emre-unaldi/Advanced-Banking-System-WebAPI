using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CreditRepository : EfRepositoryBase<Credit, int, BaseDbContext>, ICreditRepository
{
    public CreditRepository(BaseDbContext context) : base(context)
    {
    }
}
