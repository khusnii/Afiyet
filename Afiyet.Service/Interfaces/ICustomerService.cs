using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Customers;
using Afiyet.Service.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<BaseResponse<Customer>> CreateAsync(CustomerForCreationDto cashierDto);
        Task<BaseResponse<Customer>> UpdateAsync(Guid id, CustomerForCreationDto cashierDto);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Customer, bool>> expression);
        Task<BaseResponse<Customer>> GetAsync(Expression<Func<Customer, bool>> expression);
        Task<BaseResponse<IEnumerable<Customer>>> GetAllAsync(PaginationParams @params, Expression<Func<Customer, bool>> expression = null);


    }
}
