using Eszi.Demo.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Eszi.Demo.Database
{
    public class CoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public CoreDbContext(DbContextOptions<CoreDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
