using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Klir.TechChallenge.Infra.Data
{
    public class KlirCommerceContext : DbContext
    {
        public KlirCommerceContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
