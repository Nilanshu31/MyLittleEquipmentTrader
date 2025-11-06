using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;  // Add this for IQueryable

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        IQueryable<Manufacturer> AsQueryable();
        IQueryable<Manufacturer> GetQueryable();

        Task<IEnumerable<Manufacturer>> GetByFilterAsync(string manufacturerName, string country);
    }
}
