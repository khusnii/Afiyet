using Afiyet.Data.Contexts;
using Afiyet.Data.IRepositories;
using Afiyet.Domain.Entities.Locations;

namespace Afiyet.Data.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(AfiyetDbContext context) : base(context)
        {
        }
    }
}
