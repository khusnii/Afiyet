using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Locations;
using Afiyet.Domain.Enums;
using Afiyet.Service.DTOs.Locations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Service.Interfaces
{
    public interface ILocationService
    {
        Task<BaseResponse<Location>> CreateAsync(LocationForCreationDto locationDto);
        Task<BaseResponse<Location>> UpdateAsync(Guid id, LocationForCreationDto cashierDto);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Location, bool>> expression);
        Task<BaseResponse<Location>> GetAsync(Expression<Func<Location, bool>> expression);
        Task<BaseResponse<IEnumerable<Location>>> GetAllAsync(PaginationParams @params, Expression<Func<Location, bool>> expression = null);


        Task<BaseResponse<Location>> OrderPlaceWaiterAsync(Guid id, Guid waiter);
        Task<BaseResponse<Location>> OrderPlaceCustomerAsync(Guid id, Guid customer);
        Task<BaseResponse<Location>> ReleasePlaceAsync(Guid id, Guid CashierId);

    }
}
