using DbContextFactoryWebApiApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DbContextFactoryWebApiApp.DataAccess
{
    public class CRMContext : DbContext
    {
        private readonly DbContextOptions _contextOptions;

        public CRMContext()
            : base()
        {
        }

        public CRMContext(DbContextOptions options)
            : base(options)
        {
            _contextOptions = options;
        }

        public DbSet<Customer> Customers
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(e => e.CustomerId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var tenants = new string[] { "TenantA", "TenantB" };
                var connectionString = "Server=(localdb)\\mssqllocaldb;Database={tenant};Trusted_Connection=True;MultipleActiveResultSets=true";
                optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"));

                //foreach (string tenant in tenants)
                //{
                //    optionsBuilder.UseSqlServer(connectionString.Replace("{tenant}", "TenantA"));
                //}
            }
        }
    }
}
