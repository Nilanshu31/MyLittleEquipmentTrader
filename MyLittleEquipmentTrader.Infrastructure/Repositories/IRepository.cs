using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Get IQueryable for dynamic queries (no includes)
        IQueryable<T> GetQueryable();

        // Get IQueryable with optional includes (new method)
        IQueryable<T> GetQueryableWithIncludes(params Expression<Func<T, object>>[] includes);

        // Get all entities asynchronously
        Task<IEnumerable<T>> GetAllAsync();

        // Removed AddAsync, UpdateAsync, DeleteAsync, GetByIdAsync to match "get all and filter only"
    }
}
