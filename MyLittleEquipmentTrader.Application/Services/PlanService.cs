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
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _unitOfWork.Plans.GetAllAsync();
        }

        public async Task<IEnumerable<Plan>> GetByFilterAsync(string planDisplayName, string planCategoryAssignment)
        {
            return await _unitOfWork.Plans.GetByFilterAsync(planDisplayName, planCategoryAssignment);
        }

        public async Task<PagedResult<Plan>> GetPlansFilteredAsync(ProductFilterRequest filterRequest)
        {
            var query = _unitOfWork.Plans.AsQueryable();

            // Apply filters dynamically
            query = FilterHelper.ApplyFilters(query, filterRequest.Filters);

            // Apply sorting dynamically
            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            // Total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var items = await query
                .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize);

            return new PagedResult<Plan>
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize
            };
        }
    }
}
