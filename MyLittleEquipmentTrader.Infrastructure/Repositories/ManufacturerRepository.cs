using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public ManufacturerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Return all manufacturers asynchronously
        public async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        // Return IQueryable for filtering, sorting, includes etc.
        public IQueryable<Manufacturer> GetQueryable()
        {
            return _context.Manufacturers.AsQueryable();
        }
        public IQueryable<Manufacturer> AsQueryable()
        {
            return _context.Manufacturers.AsQueryable();
        }


        // Return IQueryable with eager loading of specified related entities
        public IQueryable<Manufacturer> GetQueryableWithIncludes(params Expression<Func<Manufacturer, object>>[] includes)
        {
            IQueryable<Manufacturer> query = _context.Manufacturers.AsQueryable();

            if (includes != null)
            {
                foreach (var includeExpression in includes)
                {
                    query = query.Include(includeExpression);
                }
            }

            return query;
        }

        // Custom method to filter manufacturers by name and country
        public async Task<IEnumerable<Manufacturer>> GetByFilterAsync(string manufacturerName, string country)
        {
            IQueryable<Manufacturer> query = _context.Manufacturers;

            if (!string.IsNullOrEmpty(manufacturerName))
            {
                query = query.Where(m => m.Name.Contains(manufacturerName));
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(m => m.Country.Contains(country));
            }

            return await query.ToListAsync();
        }
    }
}
