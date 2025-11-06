using Humanizer;
using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Application.Helpers;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
        {
            var reports = await _unitOfWork.Reports.GetQueryable()
                .ToListAsync();

            return reports.Select(MapToDto).ToList();
        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            var report = await _unitOfWork.Reports.GetByIdAsync(reportId);
            if (report == null)
                return false;

            report.IsDeleted = true;
            report.ModifiedDate = DateTime.UtcNow;
            report.ModifiedBy = "System"; // Replace with actual user if available

            _unitOfWork.Reports.Update(report);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedResult<ReportDto>> GetFilteredReportsAsync(ProductFilterRequest filterRequest)
        {
            filterRequest ??= new ProductFilterRequest();

            var query = _unitOfWork.Reports.GetQueryable();

            // Apply filtering if helper exists
            // query = FilterHelper.ApplyFilters(query, filterRequest.Filters);

            // Apply sorting if helper exists
            // query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                .Take(filterRequest.PageSize)
                .ToListAsync();

            var totalPages = filterRequest.PageSize <= 0
                ? 1
                : (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize);

            var dtos = items.Select(MapToDto).ToList();

            return new PagedResult<ReportDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize <= 0 ? totalCount : filterRequest.PageSize
            };
        }

        public async Task<ReportDto> AddReportAsync(ReportDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var now = DateTime.UtcNow;

            var report = new Report
            {
                TenantID = dto.TenantID,
                ReportType = dto.ReportType ?? string.Empty,
                ReportName = dto.ReportName ?? string.Empty,
                IsPublic = dto.IsPublic,
                DashboardWidgetID = dto.DashboardWidgetID ?? string.Empty,
                CustomColumns = dto.CustomColumns ?? string.Empty,
                AccessRoles = dto.AccessRoles ?? string.Empty,
                ReportPeriodEnd = dto.ReportPeriodEnd ?? DateTime.UtcNow,
                ModifiedDate = dto.ModifiedDate ?? DateTime.UtcNow,
                Filters = dto.Filters ?? string.Empty,
                GroupBy = dto.GroupBy ?? string.Empty,
                SortBy = dto.SortBy ?? string.Empty,
                IsScheduled = dto.IsScheduled,
                ExportFormat = dto.ExportFormat ?? string.Empty,
                ExportUrl = dto.ExportUrl ?? string.Empty,
                CreatedDate = dto.CreatedDate ?? DateTime.UtcNow,
                //ModifiedDate = dto.ModifiedDate ?? DateTime.UtcNow,
                ModifiedBy = dto.ModifiedBy ?? "System",
                IsDeleted = false,
                TotalSales = dto.TotalSales,
                TotalRevenue = dto.TotalRevenue,
                TotalOrders = dto.TotalOrders,
                AverageSalePrice = dto.AverageSalePrice,
                TotalFeesCollected = dto.TotalFeesCollected,
                Currency = string.IsNullOrEmpty(dto.Currency) ? "USD" : dto.Currency,
                IncludeDiscounts = dto.IncludeDiscounts,
                RegionWiseSales = dto.RegionWiseSales ?? string.Empty,
                TotalListingsSold = dto.TotalListingsSold,
                TopListings = dto.TopListings ?? string.Empty,
                TopCategories = dto.TopCategories ?? string.Empty,
                InventoryTurnover = dto.InventoryTurnover,
                RecordCount = dto.RecordCount,
                TopSellers = dto.TopSellers ?? string.Empty,
                TopBuyers = dto.TopBuyers ?? string.Empty,
                UserSegment = dto.UserSegment ?? string.Empty,
                SellerPerformance = dto.SellerPerformance ?? string.Empty,
                PageViews = dto.PageViews
            };

            await _unitOfWork.Reports.AddAsync(report);
            await _unitOfWork.CommitAsync();

            return MapToDto(report);
        }

        private ReportDto MapToDto(Report report)
        {
            return new ReportDto
            {
                ReportID = report.ReportID,
                TenantID = report.TenantID,
                ReportType = report.ReportType,
                ReportName = report.ReportName,
                CreatedDate = report.CreatedDate,
                ModifiedDate = report.ModifiedDate,
                ModifiedBy = report.ModifiedBy,
                IsDeleted = report.IsDeleted,
                IsPublic = report.IsPublic,
                DashboardWidgetID = report.DashboardWidgetID,
                CustomColumns = report.CustomColumns,
                AccessRoles = report.AccessRoles,
                ReportPeriodStart = report.ReportPeriodStart,
                ReportPeriodEnd = report.ReportPeriodEnd,
                Filters = report.Filters,
                GroupBy = report.GroupBy,
                SortBy = report.SortBy,
                IsScheduled = report.IsScheduled,
                ExportFormat = report.ExportFormat,
                ExportUrl = report.ExportUrl,
                TotalSales = report.TotalSales,
                TotalRevenue = report.TotalRevenue,
                TotalOrders = report.TotalOrders,
                AverageSalePrice = report.AverageSalePrice,
                TotalFeesCollected = report.TotalFeesCollected,
                Currency = report.Currency,
                IncludeDiscounts = report.IncludeDiscounts,
                RegionWiseSales = report.RegionWiseSales,
                TotalListingsSold = report.TotalListingsSold,
                TopListings = report.TopListings,
                TopCategories = report.TopCategories,
                InventoryTurnover = report.InventoryTurnover,
                RecordCount = report.RecordCount,
                TopSellers = report.TopSellers,
                TopBuyers = report.TopBuyers,
                UserSegment = report.UserSegment,
                SellerPerformance = report.SellerPerformance,
                PageViews = report.PageViews
            };
        }
    }
}
