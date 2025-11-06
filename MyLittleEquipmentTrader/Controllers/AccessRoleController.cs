using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessRoleController : ControllerBase
    {
        private readonly IAccessRoleService _accessRoleService;

        public AccessRoleController(IAccessRoleService accessRoleService)
        {
            _accessRoleService = accessRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _accessRoleService.GetAllAsync();
            return Ok(roles);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterAccessRoles([FromBody] ProductFilterRequest filterRequest)
        {
            if (filterRequest == null)
                return BadRequest("FilterRequest cannot be null");

            var pagedResult = await _accessRoleService.GetAccessRolesFilteredAsync(filterRequest);
            return Ok(pagedResult);
        }

    }
}
