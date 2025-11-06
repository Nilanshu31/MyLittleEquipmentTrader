using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        IQueryable<Category> AsQueryable();

        // Optional: add specific filtering methods if needed
        Task<IEnumerable<Category>> GetByFilterAsync(string someFilter);

        // ✅ Add a new category
        Task AddAsync(Category category);

        // ✅ Update an existing category (for soft delete or edits)
        Task UpdateAsync(Category category);
    }
}
