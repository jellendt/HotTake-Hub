using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotTake_Hub_Backend.Contexts
{
    public class DbHotTakeContextFactory : IDesignTimeDbContextFactory<DbHotTakeContext>
    {
        public DbHotTakeContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DbHotTakeContext> optionsBuilder = new();

            optionsBuilder.UseSqlServer("Server=...");

            return new DbHotTakeContext(optionsBuilder.Options);
        }
    }
}
