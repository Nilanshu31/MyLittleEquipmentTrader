using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public interface IAccessRoleRepository : IRepository<AccessRole>
    {
        IQueryable<AccessRole> GetQueryable();
        Task<IEnumerable<AccessRole>> GetByFilterAsync(string roleName, string roleType);
    }
}
