using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        // GET: /api/plan
        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _planService.GetAllAsync();
            return Ok(plans);
        }

        // POST: /api/plan/filter
        [HttpPost("filter")]
        public async Task<IActionResult> FilterPlans([FromBody] ProductFilterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var pagedPlans = await _planService.GetPlansFilteredAsync(request);
            return Ok(pagedPlans);
        }
    }
}
