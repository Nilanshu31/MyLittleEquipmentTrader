using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Application.Helpers;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync()
        {
            return await _unitOfWork.Tenants.GetAllAsync();
        }

        // Optional: You can add other simple methods here if needed

        // Method to get filtered, sorted, and paged Tenants
        public async Task<PagedResult<Tenant>> GetTenantsFilteredAsync(ProductFilterRequest filterRequest)
        {
            var query = _unitOfWork.Tenants.GetQueryable();

            if (filterRequest.Filters != null && filterRequest.Filters.Any())
            {
                query = FilterHelper.ApplyFilters(query, filterRequest.Filters);
            }

            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            var totalCount = await query.CountAsync();

            var items = filterRequest.PageSize <= 0
                ? await query.ToListAsync()
                : await query
                    .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                    .Take(filterRequest.PageSize)
                    .ToListAsync();

            var totalPages = filterRequest.PageSize <= 0
                ? 1
                : (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize);

            return new PagedResult<Tenant>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize <= 0 ? totalCount : filterRequest.PageSize
            };
        }
    }
}
