using Afiyet.Data.IRepositories;
using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Domain.Enums;
using Afiyet.Service.DTOs.Cashiers;
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
    public class CashierService : ICashierService
    {
#pragma warning disable

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CashierService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Cashier>> CreateAsync(CashierForCreationDto cashierDto)
        {
            var response = new BaseResponse<Cashier>();

            var cashierExist = await unitOfWork.Cashiers.GetAsync(c => c.Phone == cashierDto.Phone);
            if (cashierExist is not null)
            {
                response.Error = new ErrorResponse(400, "Cashier is exist");
                return response;
            }

            var mappedCashier = mapper.Map<Cashier>(cashierDto);

            // save image from dto model to wwwroot
            if (cashierDto.Image is not null)
                mappedCashier.Image = await FileExtensions.SaveFileAsync(cashierDto.Image.OpenReadStream(), cashierDto.Image.FileName, "ImageUrl", env, config);

            var resultCashier = await unitOfWork.Cashiers.CreateAsync(mappedCashier);

            await unitOfWork.SaveChangeAsync();

            response.Data = resultCashier;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Cashier, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var cashierExist = await unitOfWork.Cashiers.GetAsync(expression);
            if (cashierExist is null || cashierExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }
            cashierExist.Delete();

            unitOfWork.Cashiers.Update(cashierExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<Cashier>> GetAsync(Expression<Func<Cashier, bool>> expression)
        {
            var response = new BaseResponse<Cashier>();

            var cashierExist = await unitOfWork.Cashiers.GetAsync(expression);
            if (cashierExist is null || cashierExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            response.Data = cashierExist;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Cashier>>> GetAllAsync(PaginationParams @params, Expression<Func<Cashier, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Cashier>>();

            var cashiers = unitOfWork.Cashiers.GetAll(expression);

            response.Data = cashiers.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Cashier>> UpdateAsync(Guid id, CashierForCreationDto cashierDto)
        {
            var response = new BaseResponse<Cashier>();

            var cashierExist = await unitOfWork.Cashiers.GetAsync(p => p.Id == id);

            if (cashierExist is null || cashierExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            cashierExist = mapper.Map(cashierDto, cashierExist);
            cashierExist.Update();

            var result = unitOfWork.Cashiers.Update(cashierExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;


        }

    }
}
