using Afiyet.Data.Contexts;
using Afiyet.Data.IRepositories;
using Afiyet.Domain.Entities.Customers;

namespace Afiyet.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AfiyetDbContext context) : base(context)
        {
        }
    }
}
