using System;
using System.Threading.Tasks;

namespace Afiyet.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        ICashierRepository Cashiers { get; }

        IWaiterRepository Waiters { get; }

        ILocationRepository Locations { get; }

        Task SaveChangeAsync();
    }
}
