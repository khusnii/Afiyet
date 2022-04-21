using Afiyet.Data.Contexts;
using Afiyet.Data.IRepositories;
using Afiyet.Domain.Entities.Employees;

namespace Afiyet.Data.Repositories
{
    public class WaiterRepository : GenericRepository<Waiter>, IWaiterRepository
    {
        public WaiterRepository(AfiyetDbContext context) : base(context)
        {
        }
    }
}
