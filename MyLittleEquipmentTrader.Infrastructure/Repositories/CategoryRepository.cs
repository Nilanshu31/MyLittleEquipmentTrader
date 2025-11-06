using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Domain.Repositories;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                //.Include(c => c.RelatedEntity) // Include related entities if needed
                .ToListAsync();
        }

        public IQueryable<Category> AsQueryable()
        {
            return _context.Categories
                //.Include(c => c.RelatedEntity) // Include related entities if needed
                .AsQueryable();
        }

        // Optional filter method - customize as needed
        public async Task<IEnumerable<Category>> GetByFilterAsync(string someFilter)
        {
            return await _context.Categories
                .Where(c => c.CategoryName.Contains(someFilter))
                .ToListAsync();
        }

        // ✅ Add a new category
        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        // ✅ Update an existing category (soft delete or edits)
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await Task.CompletedTask; // EF tracks changes, CommitAsync will save them
        }
    }
}
