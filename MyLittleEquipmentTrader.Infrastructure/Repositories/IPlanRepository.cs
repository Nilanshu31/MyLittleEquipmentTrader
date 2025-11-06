using MyLittleEquipmentTrader.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public interface IPlanRepository : IRepository<Plan>
    {
        IQueryable<Plan> AsQueryable();
        Task<IEnumerable<Plan>> GetByFilterAsync(string category, string billingModel);
    }
}
