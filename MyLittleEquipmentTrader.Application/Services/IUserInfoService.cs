using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfoDto>> GetAllUserInfosAsync();
        Task<PagedResult<UserInfoDto>> GetFilteredUserInfosAsync(ProductFilterRequest filterRequest);
    }
}
