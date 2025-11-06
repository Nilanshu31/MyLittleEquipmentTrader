using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Plan> AsQueryable()
        {
            return _context.Plans.AsQueryable();
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetByFilterAsync(string category, string billingModel)
        {
            var query = _context.Plans.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.PlanCategoryAssignment.Contains(category));
            }

            if (!string.IsNullOrWhiteSpace(billingModel))
            {
                query = query.Where(p => p.BillingModel.Contains(billingModel));
            }

            return await query.ToListAsync();
        }

        public IQueryable<Plan> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Plan> GetQueryableWithIncludes(params Expression<Func<Plan, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        // Implement IRepository methods here if not already implemented
    }
}
