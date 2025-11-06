using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface ISalesOrderService
    {
        // Retrieves all sales orders
        Task<IEnumerable<SalesOrderDto>> GetAllSalesOrdersAsync();

        // Retrieves filtered sales orders with pagination
        Task<PagedResult<SalesOrderDto>> GetFilteredSalesOrdersAsync(ProductFilterRequest filterRequest);
    }
}
