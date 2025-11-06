// MyLittleEquipmentTrader.Application/Services/IManufacturerService.cs

using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetAllAsync();
        Task<IEnumerable<Manufacturer>> GetByFilterAsync(string manufacturerName, string country);
        Task<PagedResult<Manufacturer>> GetManufacturersFilteredAsync(ProductFilterRequest filterRequest);

    }
}
