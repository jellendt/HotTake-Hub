using HotTake_Hub_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotTake_Hub_Backend.Contexts
{
    public class DbHotTakeContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<HotTake> HotTakes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotTake>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired();
                entity.Property(e => e.AuthorId).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
}
