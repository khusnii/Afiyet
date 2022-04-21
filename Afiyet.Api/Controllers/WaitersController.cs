using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Service.DTOs.Waiters;
using Afiyet.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Afiyet.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class WaitersController : ControllerBase
    {
        private readonly IWaiterService waiterService;

        public WaitersController(IWaiterService waiterService)
        {
            this.waiterService = waiterService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Waiter>>> Create([FromForm] WaiterForCreationDto customerDto)
        {
            var result = await waiterService.CreateAsync(customerDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Waiter>>> Get(Guid id)
        {
            var result = await waiterService.GetAsync(c => c.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Waiter>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await waiterService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Waiter>>> Update(Guid id, [FromForm] WaiterForCreationDto customerDto)
        {
            var result = await waiterService.UpdateAsync(id, customerDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await waiterService.DeleteAsync(ca => ca.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
