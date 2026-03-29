using HotTake_Hub_Backend.DependencyInjection;
using HotTake_Hub_Backend.Entities;

namespace HotTake_Hub_Backend.Services.HotTakeService
{
    public interface IHotTakeService : IScopedDependency
    {
        Task<List<HotTake>> AddAsync(List<HotTake> hotTakes);
        Task<List<HotTake>> GetAllAsync();
        Task<HotTake?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<HotTake>> GetByAuthorId(Guid authorId, CancellationToken cancellationToken = default);
    }
}
