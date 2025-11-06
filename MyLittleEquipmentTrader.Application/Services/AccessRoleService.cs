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
    public class AccessRoleService : IAccessRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccessRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AccessRole>> GetAllAsync()
        {
            return await _unitOfWork.AccessRoles.GetAllAsync();
        }

        public async Task<IEnumerable<AccessRole>> GetByFilterAsync(string roleName, string roleType)
        {
            return await _unitOfWork.AccessRoles.GetByFilterAsync(roleName, roleType);
        }

        // New method to get filtered, sorted, and paged AccessRoles
        public async Task<PagedResult<AccessRole>> GetAccessRolesFilteredAsync(ProductFilterRequest filterRequest)
        {
            var query = _unitOfWork.AccessRoles.GetQueryable();

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

            return new PagedResult<AccessRole>
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
