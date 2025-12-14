using Bookify.Domain.Entities.Books;
using Bookify.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Bookify.Infrastructure.Database.Seed;

namespace Bookify.Infrastructure.Database
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.HasDefaultSchema(Schemas.Default);

            DatabaseSeeder.SeedData(modelBuilder);
        }
    }
}
