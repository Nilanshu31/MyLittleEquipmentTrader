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
    public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            return await _unitOfWork.Manufacturers.GetAllAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetByFilterAsync(string manufacturerName, string country)
        {
            return await _unitOfWork.Manufacturers.GetByFilterAsync(manufacturerName, country);
        }

        // New method to get filtered, sorted, and paged Manufacturers
        public async Task<PagedResult<Manufacturer>> GetManufacturersFilteredAsync(ProductFilterRequest filterRequest)
        {
            var query = _unitOfWork.Manufacturers.AsQueryable();

            // Apply filters from FilterHelper
            query = FilterHelper.ApplyFilters(query, filterRequest.Filters);

            // Apply sorting from FilterHelper
            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            // Get total count before paging
            var totalCount = await query.CountAsync();

            // Apply paging
            var items = await query
                .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize)
                .ToListAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize);

            return new PagedResult<Manufacturer>
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
