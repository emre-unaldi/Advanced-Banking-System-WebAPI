using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IActivityRepository : IAsyncRepository<Activity, int>, IRepository<Activity, int>
{
}
