using Afiyet.Data.IRepositories;
using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Customers;
using Afiyet.Domain.Enums;
using Afiyet.Service.DTOs.Customers;
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
    public class CustomerService : ICustomerService
    {
       
#pragma warning disable
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Customer>> CreateAsync(CustomerForCreationDto customerDto)
        {
            var response = new BaseResponse<Customer>();

            var customerExist = await unitOfWork.Customers.GetAsync(c => c.Phone == customerDto.Phone);
            if (customerExist is not null)
            {
                response.Error = new ErrorResponse(400, "Customer is exist");
                return response;
            }

            var mappedCustomer = mapper.Map<Customer>(customerDto);

            // save image from dto model to wwwroot
            if (customerDto.Image is not null)
                mappedCustomer.Image = await FileExtensions.SaveFileAsync(customerDto.Image.OpenReadStream(), customerDto.Image.FileName, "ImageUrl", env, config);

            var result = await unitOfWork.Customers.CreateAsync(mappedCustomer);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Customer, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var customerExist = await unitOfWork.Customers.GetAsync(expression);

            if (customerExist is null || customerExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Customer not found");
                return response;
            }
            customerExist.Delete();

            unitOfWork.Customers.Update(customerExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<Customer>> GetAsync(Expression<Func<Customer, bool>> expression)
        {
            var response = new BaseResponse<Customer>();

            var customerExist = await unitOfWork.Customers.GetAsync(expression);
            if (customerExist is null || customerExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            response.Data = customerExist;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Customer>>> GetAllAsync(PaginationParams @params, Expression<Func<Customer, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Customer>>();

            var cashiers = unitOfWork.Customers.GetAll(expression);

            response.Data = cashiers.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Customer>> UpdateAsync(Guid id, CustomerForCreationDto customerDto)
        {
            var response = new BaseResponse<Customer>();

            var customerExist = await unitOfWork.Customers.GetAsync(p => p.Id == id);

            if (customerExist is null || customerExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            customerExist = mapper.Map(customerDto, customerExist);
            customerExist.Update();

            var result = unitOfWork.Customers.Update(customerExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;


        }

    }
}
