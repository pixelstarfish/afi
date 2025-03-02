using Afi.CustomerPortal.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace Afi.CustomerPortal.Services.DataContexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<CustomerPolicy> CustomerPolicies { get; set; }
    }
}