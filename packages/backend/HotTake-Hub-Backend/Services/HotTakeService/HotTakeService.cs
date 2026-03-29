using HotTake_Hub_Backend.Contexts;
using HotTake_Hub_Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotTake_Hub_Backend.Services.HotTakeService
{
    public class HotTakeService([FromServices] DbHotTakeContext dbHotTakeContext) : IHotTakeService
    {
        public async Task<List<HotTake>> AddAsync(List<HotTake> hotTakes)
        {
            await dbHotTakeContext.HotTakes.AddRangeAsync(hotTakes);
            await dbHotTakeContext.SaveChangesAsync();

            return hotTakes;
        }

        public Task<List<HotTake>> GetAllAsync()
        {
            return DefaultQuery().ToListAsync();
        }

        public Task<List<HotTake>> GetByAuthorId(Guid authorId, CancellationToken cancellationToken = default)
        {
            return DefaultQuery().Where(ht => ht.AuthorId == authorId).ToListAsync(cancellationToken);
        }

        public Task<HotTake?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return DefaultQuery().FirstOrDefaultAsync(ht => ht.Id == id, cancellationToken);
        }

        private IQueryable<HotTake> DefaultQuery()
        {
            return dbHotTakeContext.HotTakes.Where(ht => !ht.IsDeleted);
        }
    }
}
