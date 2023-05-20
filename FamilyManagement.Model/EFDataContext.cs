using Microsoft.EntityFrameworkCore;

namespace FamilyManagement.Model
{
    public class EFDataContext : DbContext
    {
        public DbSet<Family> Families { get; protected set; }

        public EFDataContext(string connectionString)
          : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlServer(connectionString).Options)
        {
        }
        public EFDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
               // options.UseSqlServer(connectionString);
               // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}