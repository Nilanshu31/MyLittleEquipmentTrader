// MyLittleEquipmentTrader.Application/Services/IPlanService.cs

using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetAllAsync();

        Task<IEnumerable<Plan>> GetByFilterAsync(string planDisplayName, string planCategoryAssignment);

        Task<PagedResult<Plan>> GetPlansFilteredAsync(ProductFilterRequest filterRequest);
    }
}
