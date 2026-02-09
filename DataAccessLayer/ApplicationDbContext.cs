using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // Define DbSet properties for your entities here
        // For example:
        // public DbSet<YourEntity> YourEntities { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity relationships and constraints here
            // For example:
            // modelBuilder.Entity<YourEntity>()
            //     .HasOne(e => e.RelatedEntity)
            //     .WithMany()
            //     .HasForeignKey(e => e.RelatedEntityId);
        }
    }
}
