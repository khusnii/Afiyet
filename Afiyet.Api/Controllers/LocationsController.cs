using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Locations;
using Afiyet.Service.DTOs.Locations;
using Afiyet.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Afiyet.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationsController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Location>>> Create([FromForm] LocationForCreationDto locationDto)
        {
            var result = await locationService.CreateAsync(locationDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Location>>> Get([FromRoute] Guid id)
        {
            var result = await locationService.GetAsync(c => c.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Location>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await locationService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Location>>> Update(Guid id, [FromForm] LocationForCreationDto locationDto)
        {
            var result = await locationService.UpdateAsync(id, locationDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await locationService.DeleteAsync(ca => ca.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPatch("release{id}")]
        public async Task<ActionResult<BaseResponse<Location>>> Release(Guid id, Guid cashierId)
        {
            var result = await locationService.ReleasePlaceAsync(id, cashierId);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPatch("oreder/waiter{id}")]
        public async Task<ActionResult<BaseResponse<Location>>> OrderWaiter(Guid id, Guid waiter)
        {
            var result = await locationService.OrderPlaceWaiterAsync(id, waiter);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPatch("oreder/customer{id}")]
        public async Task<ActionResult<BaseResponse<Location>>> OrderCustomer(Guid id, Guid waiter)
        {
            var result = await locationService.OrderPlaceCustomerAsync(id, waiter);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
