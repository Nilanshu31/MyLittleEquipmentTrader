using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<PagedResult<CategoryDto>> GetFilteredCategoriesAsync(ProductFilterRequest filterRequest);

        // Create a new category
        Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto);

        // Soft delete a category
        Task<bool> DeleteCategoryAsync(int categoryId);

        // Optional: you can later add GetById, Update methods
        // Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        // Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryUpdateDto categoryUpdateDto);
    }
}
