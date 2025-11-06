using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet]
        
        // Neem ka Patta Kadva Hai 
        [Authorize(Policy = "CanViewCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // POST: api/category/filter
        [HttpPost("filter")]
        [Authorize(Policy = "CanFilterCategories")]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetFilteredCategories([FromBody] ProductFilterRequest filterRequest)
        {
            if (filterRequest == null)
            {
                return BadRequest("Filter request cannot be null.");
            }

            var filteredCategories = await _categoryService.GetFilteredCategoriesAsync(filterRequest);
            return Ok(filteredCategories);
        }

        // POST: api/category
        [HttpPost]
        [Authorize(Policy = "CanCreateCategories")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest("Category data is required.");

            var createdCategory = await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetAllCategories), new { id = createdCategory.CategoryID }, createdCategory);
        }

        // DELETE: api/category/{id}jhjhj
        [HttpDelete("{id}")]
        [Authorize(Policy = "CanDeleteCategories")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            if (!deleted) return NotFound();

            return NoContent(); // soft delete
        }
    }
}
