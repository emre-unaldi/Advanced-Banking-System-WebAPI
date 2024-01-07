using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ISupportRepository : IAsyncRepository<Support, int>, IRepository<Support, int>
{
}