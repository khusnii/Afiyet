using Afiyet.Data.IRepositories;
using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Domain.Enums;
using Afiyet.Service.DTOs.Waiters;
using Afiyet.Service.Extensions;
using Afiyet.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Afiyet.Service.Services
{
    public class WaiterService : IWaiterService
    {
#pragma warning disable

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public WaiterService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Waiter>> CreateAsync(WaiterForCreationDto waiterDto)
        {
            var response = new BaseResponse<Waiter>();

            var waiterExist = await unitOfWork.Waiters.GetAsync(c => c.Phone == waiterDto.Phone);
            if (waiterExist is not null)
            {
                response.Error = new ErrorResponse(400, "Waiter is exist");
                return response;
            }

            var mappedWaiter = mapper.Map<Waiter>(waiterDto);

            // save image from dto model to wwwroot
            if (waiterDto.Image is not null)
                mappedWaiter.Image = await FileExtensions.SaveFileAsync(waiterDto.Image.OpenReadStream(), waiterDto.Image.FileName, "ImageUrl", env, config);

            var result = await unitOfWork.Waiters.CreateAsync(mappedWaiter);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Waiter, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var customerExist = await unitOfWork.Waiters.GetAsync(expression);

            if (customerExist is null || customerExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Customer not found");
                return response;
            }
            customerExist.Delete();

            unitOfWork.Waiters.Update(customerExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<Waiter>> GetAsync(Expression<Func<Waiter, bool>> expression)
        {
            var response = new BaseResponse<Waiter>();

            var customerExist = await unitOfWork.Waiters.GetAsync(expression);
            if (customerExist is null || customerExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            response.Data = customerExist;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Waiter>>> GetAllAsync(PaginationParams @params, Expression<Func<Waiter, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Waiter>>();

            var cashiers = unitOfWork.Waiters.GetAll(expression);

            response.Data = cashiers.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Waiter>> UpdateAsync(Guid id, WaiterForCreationDto customerDto)
        {
            var response = new BaseResponse<Waiter>();

            var waiterExist = await unitOfWork.Waiters.GetAsync(p => p.Id == id);

            if (waiterExist is null || waiterExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            waiterExist = mapper.Map(customerDto, waiterExist);
            waiterExist.Update();

            var result = unitOfWork.Waiters.Update(waiterExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;


        }

    }
}
