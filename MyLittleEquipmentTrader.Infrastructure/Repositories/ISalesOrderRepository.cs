using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Domain.Repositories
{
    public interface ISalesOrderRepository
    {
        // Existing methods
        Task<IEnumerable<SalesOrder>> GetAllAsync();
        IQueryable<SalesOrder> AsQueryable();

        // New filtering method (based on your error)
        Task<IEnumerable<SalesOrder>> GetByFilterAsync(string orderNumber = null, int? productId = null, decimal? minAmount = null, decimal? maxAmount = null);
    }
}
