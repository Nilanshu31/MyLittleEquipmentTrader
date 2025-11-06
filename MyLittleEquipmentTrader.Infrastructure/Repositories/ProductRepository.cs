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
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Return all products asynchronously
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Return IQueryable for filtering, sorting, includes etc.
        public IQueryable<Product> GetQueryable()
        {
            return _context.Products.AsQueryable();
        }

        // Return IQueryable with eager loading of specified related entities
        public IQueryable<Product> GetQueryableWithIncludes(params Expression<Func<Product, object>>[] includes)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (includes != null)
            {
                foreach (var includeExpression in includes)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query;
        }

        // Removed AddAsync, UpdateAsync, DeleteAsync, GetByIdAsync as per your "get all and filter only" request
    }
}
