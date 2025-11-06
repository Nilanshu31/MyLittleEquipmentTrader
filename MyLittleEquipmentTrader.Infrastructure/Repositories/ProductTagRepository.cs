using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class ProductTagRepository : IRepository<ProductTag>
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the context
        public ProductTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Return all ProductTags asynchronously
        public async Task<IEnumerable<ProductTag>> GetAllAsync()
        {
            return await _context.ProductTags.ToListAsync();
        }

        // Return IQueryable for filtering, sorting, includes, etc.
        public IQueryable<ProductTag> GetQueryable()
        {
            return _context.ProductTags.AsQueryable();
        }

        // Return IQueryable with eager loading of specified related entities
        public IQueryable<ProductTag> GetQueryableWithIncludes(params Expression<Func<ProductTag, object>>[] includes)
        {
            IQueryable<ProductTag> query = _context.ProductTags.AsQueryable();

            if (includes != null)
            {
                foreach (var includeExpression in includes)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query;
        }

        // Add, Update, Delete, and Get by ID (if needed)
        public async Task AddAsync(ProductTag entity)
        {
            await _context.ProductTags.AddAsync(entity);
        }

        public async Task UpdateAsync(ProductTag entity)
        {
            _context.ProductTags.Update(entity);
            await Task.CompletedTask;  // Keeps async signature
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProductTags.FindAsync(id);
            if (entity != null)
            {
                _context.ProductTags.Remove(entity);
            }
        }

        public async Task<ProductTag> GetByIdAsync(int id)
        {
            return await _context.ProductTags.FindAsync(id);
        }
    }
}
