using Afiyet.Data.IRepositories;
using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Locations;
using Afiyet.Domain.Enums;
using Afiyet.Service.DTOs.Locations;
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
    public class LocationService : ILocationService
    {

#pragma warning disable

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Location>> CreateAsync(LocationForCreationDto locationDto)
        {
            var response = new BaseResponse<Location>();

            var locationExist = await unitOfWork.Locations.GetAsync(c => c.PlaceDigit == locationDto.PlaceDigit && c.PlaceType == locationDto.PlaceType);

            if (locationExist is not null)
            {
                response.Error = new ErrorResponse(400, "Location is exist");
                return response;
            }

            var mappedLocation = mapper.Map<Location>(locationDto);

            // save image from dto model to wwwroot
            if (locationDto.Image is not null)
                mappedLocation.Image = await FileExtensions.SaveFileAsync(locationDto.Image.OpenReadStream(), locationDto.Image.FileName, "ImageUrl", env, config);

            var result = await unitOfWork.Locations.CreateAsync(mappedLocation);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Location, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var locationExist = await unitOfWork.Locations.GetAsync(expression);

            if (locationExist is null || locationExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Location not found");
                return response;
            }
            locationExist.Delete();

            unitOfWork.Locations.Update(locationExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<Location>> GetAsync(Expression<Func<Location, bool>> expression)
        {
            var response = new BaseResponse<Location>();

            var locationExist = await unitOfWork.Locations.GetAsync(expression);
            if (locationExist is null || locationExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Location not found");
                return response;
            }

            response.Data = locationExist;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Location>>> GetAllAsync(PaginationParams @params, Expression<Func<Location, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<Location>>();

            var cashiers = unitOfWork.Locations.GetAll(expression);

            response.Data = cashiers.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Location>> UpdateAsync(Guid id, LocationForCreationDto locationDto)
        {
            var response = new BaseResponse<Location>();

            var locationExist = await unitOfWork.Locations.GetAsync(p => p.Id == id);

            if (locationExist is null || locationExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Location not found");
                return response;
            }

            locationExist = mapper.Map(locationDto, locationExist);
            locationExist.Update();

            var result = unitOfWork.Locations.Update(locationExist);

            await unitOfWork.SaveChangeAsync();

            response.Data = result;

            return response;

        }

        public async Task<BaseResponse<Location>> OrderPlaceWaiterAsync(Guid id, Guid waiter)
        {
            var response = new BaseResponse<Location>();
            var locationExist = await unitOfWork.Locations.GetAsync(l => l.Id == id);

            if (locationExist is null || locationExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Location not found");
                return response;
            }

            var waiterExist = await unitOfWork.Waiters.GetAsync(p => p.Id == waiter);

            if (waiterExist is null)
            {
                response.Error = new ErrorResponse(404, "Waiter not found");
                return response;
            }
    
            if (locationExist.Employement == Employement.Busy)
            {
                response.Error = new ErrorResponse(400, "Location is busy");
                return response;
            }

            locationExist.Employement = Employement.Busy;

            await unitOfWork.SaveChangeAsync();

            response.Data = locationExist;

            return response;

        }

        public async Task<BaseResponse<Location>> ReleasePlaceAsync(Guid id, Guid CashierId)
        {
            var response = new BaseResponse<Location>();

            var locationExist = await unitOfWork.Locations.GetAsync(l => l.Id == id);

            if (locationExist is null || locationExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Location not found");
                return response;
            }

            var cashierExist = await unitOfWork.Cashiers.GetAsync(c => c.Id == CashierId);
          
            if (cashierExist is null || cashierExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Cashier not found");
                return response;
            }

            locationExist.Employement = Employement.Empty;

            await unitOfWork.SaveChangeAsync();

            response.Data = locationExist;

            return response;

        }

        public async Task<BaseResponse<Location>> OrderPlaceCustomerAsync(Guid id, Guid customer)
        {
            var response = new BaseResponse<Location>();
            var locationExist = await unitOfWork.Locations.GetAsync(l => l.Id == id);

            if (locationExist is null || locationExist.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Location not found");
                return response;
            }

            var customerExist = await unitOfWork.Customers.GetAsync(p => p.Id == customer);

            if (customerExist is null)
            {
                response.Error = new ErrorResponse(404, "Customer not found");
                return response;
            }

            if (locationExist.Employement == Employement.Busy)
            {
                response.Error = new ErrorResponse(400, "Location is busy");
                return response;
            }

            locationExist.Employement = Employement.Busy;

            await unitOfWork.SaveChangeAsync();

            response.Data = locationExist;

            return response;
        }
    }
}
