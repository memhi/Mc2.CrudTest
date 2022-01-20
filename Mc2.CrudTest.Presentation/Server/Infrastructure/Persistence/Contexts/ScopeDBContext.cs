using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Presentation.Server.Persistence.Contexts
{
    public class ScopeDBContext : DbContext
    {
        public ScopeDBContext(DbContextOptions<ScopeDBContext> options)
            : base(options)
        {

        }
        public DbSet<CustomerInfo> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerInfo>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<CustomerInfo>().HasIndex(p => new { p.Firstname, p.Lastname,p.DateOfBirth }).IsUnique();
            modelBuilder.Entity<CustomerInfo>().HasQueryFilter(p => !p.IsRemoved);

        }
    }
}
