using MyLittleEquipmentTrader.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        IQueryable<Subscription> AsQueryable();
        Task<IEnumerable<Subscription>> GetByFilterAsync(string someFilter);

        // ✅ New: supports eager loading with Include
        IQueryable<Subscription> GetQueryableWithIncludes(params Expression<Func<Subscription, object>>[] includes);
    }
}
