using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Application.Helpers;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models.DTOS;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<bool> DeleteSubscriptionAsync(int subscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _unitOfWork.Subscriptions
    .GetQueryableWithIncludes(s => s.Tenant, s => s.Plan)
    .ToListAsync();


            return subscriptions.Select(MapToDto).ToList();
        }

        // ✅ Updated with filter/sort/paging
        public async Task<PagedResult<SubscriptionDto>> GetFilteredSubscriptionsAsync(ProductFilterRequest filterRequest)
        {
            filterRequest ??= new ProductFilterRequest();

            // ✅ Include Tenant and Plan before converting to IQueryable
            var query = _unitOfWork.Subscriptions
      .GetQueryableWithIncludes(s => s.Tenant, s => s.Plan);


            // ✅ Apply dynamic filters
            if (filterRequest.Filters != null && filterRequest.Filters.Any())
            {
                query = FilterHelper.ApplyFilters(query, filterRequest.Filters);
            }

            // ✅ Apply sorting
            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            // ✅ Count total
            var totalCount = await query.CountAsync();

            if (filterRequest.PageSize <= 0)
                filterRequest.PageSize = totalCount == 0 ? 1 : totalCount;

            var items = await query
                .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize);

            var dtos = items.Select(MapToDto).ToList();

            return new PagedResult<SubscriptionDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize
            };
        }

        private SubscriptionDto MapToDto(Subscription s)
        {
            return new SubscriptionDto
            {
                SubscriptionId = s.SubscriptionId,
                TenantId = s.TenantId,
                PlanId = s.PlanId,
                Tenant = s.Tenant == null ? null : new TenantDto
                {
                    TenantID = s.Tenant.TenantID,
                    TenantName = s.Tenant.TenantName,
                    PrimaryContactEmail = s.Tenant.PrimaryContactEmail,
                    BrandName = s.Tenant.BrandName,
                    DomainName = s.Tenant.DomainName,
                    Subdomain = s.Tenant.Subdomain
                },
                Plan = s.Plan == null ? null : new PlanDto
                {
                    PlanId = s.Plan.PlanId,
                    PlanDisplayName = s.Plan.PlanDisplayName,
                    GlobalPlanVisibility = s.Plan.GlobalPlanVisibility
                },
                RenewalDate = s.RenewalDate,
                AutoRenewal = s.AutoRenewal,
                PlanStatus = s.PlanStatus,
                MonthlySubscriptionPrice = s.MonthlySubscriptionPrice,
                AnnualSubscriptionPrice = s.AnnualSubscriptionPrice,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            };
        }
    }
}
