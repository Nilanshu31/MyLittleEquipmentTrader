using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class ProductAttributeRepository : IRepository<ProductAttribute>
    {
        private readonly ApplicationDbContext _context;

        public ProductAttributeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all ProductAttributes asynchronously
        public async Task<IEnumerable<ProductAttribute>> GetAllAsync()
        {
            return await _context.ProductAttributes.ToListAsync();
        }

        // Return IQueryable for filtering, sorting, includes etc.
        public IQueryable<ProductAttribute> GetQueryable()
        {
            return _context.ProductAttributes.AsQueryable();
        }

        public IQueryable<ProductAttribute> GetQueryableWithIncludes(params Expression<Func<ProductAttribute, object>>[] includes)
        {
            IQueryable<ProductAttribute> query = _context.ProductAttributes.AsQueryable();

            if (includes != null)
            {
                foreach (var includeExpression in includes)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query;
        }

        // You can remove AddAsync, UpdateAsync, DeleteAsync, GetByIdAsync if you don't need them
        // If you still want them, keep these methods here.
    }
}
