using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _tenantService.GetAllAsync();
            return Ok(tenants);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterTenants([FromBody] ProductFilterRequest filterRequest)
        {
            if (filterRequest == null)
                return BadRequest("FilterRequest cannot be null");

            var pagedResult = await _tenantService.GetTenantsFilteredAsync(filterRequest);
            return Ok(pagedResult);
        }
    }
}
