using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // GET: api/subscription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetAllSubscriptions()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            return Ok(subscriptions);
        }

        // POST: api/subscription/filter
        [HttpPost("filter")]
        public async Task<ActionResult<PagedResult<SubscriptionDto>>> GetFilteredSubscriptions([FromBody] ProductFilterRequest filterRequest)
        {
            if (filterRequest == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var filteredSubscriptions = await _subscriptionService.GetFilteredSubscriptionsAsync(filterRequest);
            return Ok(filteredSubscriptions);
        }
    }
}
