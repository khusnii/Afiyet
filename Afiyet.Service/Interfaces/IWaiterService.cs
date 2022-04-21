using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Service.DTOs.Waiters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Service.Interfaces
{
    public interface IWaiterService
    {
        Task<BaseResponse<Waiter>> CreateAsync(WaiterForCreationDto cashierDto);

        Task<BaseResponse<Waiter>> UpdateAsync(Guid id, WaiterForCreationDto cashierDto);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Waiter, bool>> expression);

        Task<BaseResponse<Waiter>> GetAsync(Expression<Func<Waiter, bool>> expression);

        Task<BaseResponse<IEnumerable<Waiter>>> GetAllAsync(PaginationParams @params, Expression<Func<Waiter, bool>> expression = null);
    }
}
