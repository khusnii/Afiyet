using Afiyet.Data.Contexts;
using Afiyet.Data.IRepositories;

using Afiyet.Domain.Entities.Employees;

namespace Afiyet.Data.Repositories
{
    public class CashiersRepository : GenericRepository<Cashier>, ICashierRepository
    {
        public CashiersRepository(AfiyetDbContext context) : base(context)
        {
        }
    }
}

