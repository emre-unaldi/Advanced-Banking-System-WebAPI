using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ActivityRepository : EfRepositoryBase<Activity, int, BaseDbContext>, IActivityRepository
{
    public ActivityRepository(BaseDbContext context) : base(context)
    {
    }
}
