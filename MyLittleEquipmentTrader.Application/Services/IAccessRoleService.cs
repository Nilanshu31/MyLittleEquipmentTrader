using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface IAccessRoleService
    {
        Task<IEnumerable<AccessRole>> GetAllAsync();
        Task<PagedResult<AccessRole>> GetAccessRolesFilteredAsync(ProductFilterRequest filterRequest);
    }
}
