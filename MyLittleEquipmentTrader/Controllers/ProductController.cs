using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using MyLittleEquipmentTrader.Application.Interfaces;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Services;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "TenantAdmin,GlobalAdmin")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /api/product
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // POST: /api/product/filter
        [HttpPost("filter")]
        public async Task<IActionResult> FilterProducts([FromBody] ProductFilterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var pagedProducts = await _productService.GetFilteredProductsAsync(request);
            return Ok(pagedProducts);
        }
    }
}
