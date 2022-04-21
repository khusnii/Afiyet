using Afiyet.Domain.Commons;
using Afiyet.Domain.Configurations;
using Afiyet.Domain.Entities.Employees;
using Afiyet.Service.DTOs.Cashiers;
using Afiyet.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Afiyet.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CashiersController : ControllerBase
    {
        private readonly ICashierService cashierService;
        private readonly IWebHostEnvironment env;

        public CashiersController(ICashierService cashierService, IWebHostEnvironment env)
        {
            this.cashierService = cashierService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Cashier>>> Create([FromForm] CashierForCreationDto cashierDto)
        {
            var result = await cashierService.CreateAsync(cashierDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Cashier>>> Get(Guid id)
        {
            var result = await cashierService.GetAsync(c => c.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Cashier>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await cashierService.GetAllAsync(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Cashier>>> Update(Guid id, [FromForm] CashierForCreationDto cashierDto)
        {
            var result = await cashierService.UpdateAsync(id, cashierDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await cashierService.DeleteAsync(ca => ca.Id == id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

    }
}
