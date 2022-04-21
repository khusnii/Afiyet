
using Afiyet.Domain.Entities.Customers;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Domain.Entities.Locations;
using Microsoft.EntityFrameworkCore;

namespace Afiyet.Data.Contexts
{
    public class AfiyetDbContext : DbContext
    {
        public AfiyetDbContext(DbContextOptions<AfiyetDbContext> options)
         : base(options)
        {

        }

        public DbSet<Cashier> Cashiers { get; set; }

        public DbSet<Waiter> Waiters { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Location> Locations { get; set; }

    }
}
