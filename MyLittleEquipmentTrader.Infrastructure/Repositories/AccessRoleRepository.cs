using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class AccessRoleRepository : IAccessRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public AccessRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<AccessRole> GetQueryable()
        {
            return _context.AccessRoles.AsQueryable();
        }

        public IQueryable<AccessRole> GetQueryableWithIncludes(params Expression<Func<AccessRole, object>>[] includes)
        {
            IQueryable<AccessRole> query = _context.AccessRoles.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public async Task<IEnumerable<AccessRole>> GetAllAsync()
        {
            return await _context.AccessRoles.ToListAsync();
        }

        public async Task<IEnumerable<AccessRole>> GetByFilterAsync(string roleName, string roleType)
        {
            var query = _context.AccessRoles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(roleName))
                query = query.Where(r => r.RoleName.Contains(roleName));

            if (!string.IsNullOrWhiteSpace(roleType))
                query = query.Where(r => r.RoleType.Contains(roleType));

            return await query.ToListAsync();
        }
    }
}
