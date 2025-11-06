using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyLittleEquipmentTrader.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "TenantAdmin,GlobalAdmin")]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;

        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        // GET: api/salesorder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrderDto>>> GetAllSalesOrders()
        {
            var salesOrders = await _salesOrderService.GetAllSalesOrdersAsync();
            return Ok(salesOrders);
        }

        // POST: api/salesorder/filter
        [HttpPost("filter")]
        public async Task<ActionResult<PagedResult<SalesOrderDto>>> GetFilteredSalesOrders([FromBody] ProductFilterRequest filterRequest)
        {
            if (filterRequest == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var filteredSalesOrders = await _salesOrderService.GetFilteredSalesOrdersAsync(filterRequest);
            return Ok(filteredSalesOrders);
        }
    }
}
