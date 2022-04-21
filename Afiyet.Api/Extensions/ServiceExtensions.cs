using Afiyet.Data.IRepositories;
using Afiyet.Data.Repositories;
using Afiyet.Service.Interfaces;
using Afiyet.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Afiyet.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICashierService, CashierService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IWaiterService, WaiterService>();
            services.AddScoped<ILocationService, LocationService>();

        }
    }
}
