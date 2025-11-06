using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
        Task<PagedResult<SubscriptionDto>> GetFilteredSubscriptionsAsync(ProductFilterRequest filterRequest);

        //Task<SubscriptionDto> GetSubscriptionByIdAsync(int subscriptionId);
        //Task<SubscriptionDto> CreateSubscriptionAsync(SubscriptionDto subscriptionCreateDto);
        //Task<SubscriptionDto> UpdateSubscriptionAsync(int subscriptionId, SubscriptionUpdateDto subscriptionUpdateDto);
        Task<bool> DeleteSubscriptionAsync(int subscriptionId);
    }
}
