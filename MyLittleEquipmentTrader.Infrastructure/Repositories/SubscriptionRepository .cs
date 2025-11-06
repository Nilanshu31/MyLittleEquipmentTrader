using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Domain.Repositories;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public IQueryable<Subscription> AsQueryable()
        {
            return _context.Subscriptions.AsQueryable();
        }

        public async Task<IEnumerable<Subscription>> GetByFilterAsync(string someFilter)
        {
            return await _context.Subscriptions
                .Where(s => s.PlanStatus.Contains(someFilter))
                .ToListAsync();
        }

        // ✅ This method lets you include related entities
        public IQueryable<Subscription> GetQueryableWithIncludes(params Expression<Func<Subscription, object>>[] includes)
        {
            IQueryable<Subscription> query = _context.Subscriptions;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
