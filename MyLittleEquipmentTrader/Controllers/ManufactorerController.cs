using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using MyLittleEquipmentTrader.Domain.Entities;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        // GET: api/manufacturer
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var manufacturers = await _manufacturerService.GetAllAsync();
            return Ok(manufacturers);
        }

        // GET: api/manufacturer/filter?manufacturerName=...&country=...
        //[HttpGet("filter")]
        //public async Task<IActionResult> GetByFilter([FromQuery] string manufacturerName, [FromQuery] string country)
        //{
        //    var manufacturers = await _manufacturerService.GetByFilterAsync(manufacturerName, country);
        //    return Ok(manufacturers);
        //}

        // POST: api/manufacturer/filter
        // Accepts flexible filters, sorting, paging in body
        [HttpPost("filter")]
        public async Task<IActionResult> FilterManufacturers([FromBody] ProductFilterRequest filterRequest)
        {
            if (filterRequest == null)
                return BadRequest("FilterRequest cannot be null");

            var pagedResult = await _manufacturerService.GetManufacturersFilteredAsync(filterRequest);
            return Ok(pagedResult);
        }
    }
}
