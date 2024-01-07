using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ICreditRepository : IAsyncRepository<Credit, int>, IRepository<Credit, int>
{
}
