using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class SupportRepository : EfRepositoryBase<Support, int, BaseDbContext>, ISupportRepository
{
    public SupportRepository(BaseDbContext context) : base(context)
    {
    }
}