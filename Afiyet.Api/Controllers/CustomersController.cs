using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Customers;
using Afiyet.Service.DTOs.Customers;
using Afiyet.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Afiyet.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Customer>>> Create([FromForm] CustomerForCreationDto customerDto)
        {
            var result = await customerService.CreateAsync(customerDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Customer>>> Get([FromRoute] Guid id)
        {
            var result = await customerService.GetAsync(c => c.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Customer>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await customerService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Customer>>> Update(Guid id, [FromForm] CustomerForCreationDto customerDto)
        {
            var result = await customerService.UpdateAsync(id, customerDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await customerService.DeleteAsync(ca => ca.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
