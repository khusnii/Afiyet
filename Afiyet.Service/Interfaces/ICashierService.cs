using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Service.DTOs.Cashiers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Service.Interfaces
{
    public interface ICashierService
    {
        Task<BaseResponse<Cashier>> CreateAsync(CashierForCreationDto cashierDto);
        Task<BaseResponse<Cashier>> UpdateAsync(Guid id, CashierForCreationDto cashierDto);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Cashier, bool>> expression);
        Task<BaseResponse<Cashier>> GetAsync(Expression<Func<Cashier, bool>> expression);
        Task<BaseResponse<IEnumerable<Cashier>>> GetAllAsync(PaginationParams @params, Expression<Func<Cashier, bool>> expression = null);



    }
}
