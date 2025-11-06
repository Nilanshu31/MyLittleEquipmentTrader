using MyLittleEquipmentTrader.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        IQueryable<Tenant> GetQueryable();
        // Add any custom Tenant-specific queries here
    }
}
