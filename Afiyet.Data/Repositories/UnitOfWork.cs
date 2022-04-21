using Afiyet.Data.Contexts;
using Afiyet.Data.IRepositories;
using System;
using System.Threading.Tasks;

namespace Afiyet.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AfiyetDbContext context;

        /// <summary>
        /// Repositories
        /// </summary>
        public ICustomerRepository Customers { get; private set; }

        public ICashierRepository Cashiers { get; private set; }

        public IWaiterRepository Waiters { get; private set; }

        public ILocationRepository Locations { get; private set; }

        public UnitOfWork(AfiyetDbContext context)
        {
            this.context = context;

            // object intializing for repositories
            Customers = new CustomerRepository(context);
            Cashiers = new CashiersRepository(context);
            Waiters = new WaiterRepository(context);
            Locations = new LocationRepository(context);
        }


        public async Task SaveChangeAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
