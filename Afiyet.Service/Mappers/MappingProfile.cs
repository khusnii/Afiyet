using Afiyet.Domain.Entities.Customers;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Domain.Entities.Locations;
using Afiyet.Service.DTOs.Cashiers;
using Afiyet.Service.DTOs.Customers;
using Afiyet.Service.DTOs.Locations;
using Afiyet.Service.DTOs.Waiters;
using AutoMapper;

namespace Afiyet.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CashierForCreationDto, Cashier>().ReverseMap();
            CreateMap<CustomerForCreationDto, Customer>().ReverseMap();
            CreateMap<LocationForCreationDto, Location>().ReverseMap();
            CreateMap<WaiterForCreationDto, Waiter>().ReverseMap();
        }
    }
}
