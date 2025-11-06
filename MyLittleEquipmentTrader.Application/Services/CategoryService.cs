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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ Get all categories
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories
                .AsQueryable()
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return categories.Select(MapToDto);
        }

        // ✅ Get filtered, sorted, and paginated categories
        public async Task<PagedResult<CategoryDto>> GetFilteredCategoriesAsync(ProductFilterRequest filterRequest)
        {
            filterRequest ??= new ProductFilterRequest();

            var query = _unitOfWork.Categories
                .AsQueryable()
                .Where(c => !c.IsDeleted);

            // Apply dynamic filters and sorting using FilterHelper
            if (filterRequest.Filters != null && filterRequest.Filters.Any())
            {
                query = FilterHelper.ApplyFilters(query, filterRequest.Filters);
            }

            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            // Count before pagination
            var totalCount = await query.CountAsync();

            List<Category> categories;
            if (filterRequest.PageSize <= 0)
            {
                categories = await query.ToListAsync();
            }
            else
            {
                categories = await query
                    .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                    .Take(filterRequest.PageSize)
                    .ToListAsync();
            }

            var categoryDtos = categories.Select(MapToDto).ToList();

            // Build paginated result
            return new PagedResult<CategoryDto>
            {
                TotalCount = totalCount,
                Items = categoryDtos,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize <= 0 ? totalCount : filterRequest.PageSize,
                TotalPages = filterRequest.PageSize <= 0
                    ? 1
                    : (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize)
            };
        }

        // ✅ Add category
        public async Task<CategoryDto> AddCategoryAsync(CategoryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var category = new Category
            {
                TenantID = dto.TenantID,
                CategoryName = dto.CategoryName,
                Slug = dto.Slug,
                ParentCategoryID = dto.ParentCategoryID,
                CategoryLevel = dto.CategoryLevel,
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                ShowInMenu = dto.ShowInMenu,
                CategoryImageUrl = dto.CategoryImageUrl,
                IsFeatured = dto.IsFeatured,
                IsDiscounted = dto.IsDiscounted,
                DiscountID = dto.DiscountID,
                RelatedCategoryIDs = dto.RelatedCategoryIDs,
                ProductCount = dto.ProductCount,
                MetaTitle = dto.MetaTitle,
                MetaDescription = dto.MetaDescription,
                Tags = dto.Tags,
                LanguageCode = dto.LanguageCode,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy ?? "System",
                ModifiedBy = dto.ModifiedBy ?? "System",
                IsDeleted = false,
                AccessRoles = dto.AccessRoles,
                AvailabilitySchedule = dto.AvailabilitySchedule,
                ShowInFooter = dto.ShowInFooter,
                LayoutType = dto.LayoutType,
                ThumbnailImageUrl = dto.ThumbnailImageUrl,
                BannerImageUrl = dto.BannerImageUrl
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CommitAsync();

            return MapToDto(category);
        }

        // ✅ Delete category (soft delete)
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories
                .AsQueryable()
                .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

            if (category == null)
                return false;

            category.IsDeleted = true;
            await _unitOfWork.CommitAsync();

            return true;
        }

        // ✅ DTO Mapper
        private CategoryDto MapToDto(Category c)
        {
            return new CategoryDto
            {
                CategoryID = c.CategoryID,
                TenantID = c.TenantID,
                CategoryName = c.CategoryName,
                Slug = c.Slug,
                ParentCategoryID = c.ParentCategoryID,
                CategoryLevel = c.CategoryLevel,
                DisplayOrder = c.DisplayOrder,
                IsActive = c.IsActive,
                ShowInMenu = c.ShowInMenu,
                CategoryImageUrl = c.CategoryImageUrl,
                IsFeatured = c.IsFeatured,
                IsDiscounted = c.IsDiscounted,
                DiscountID = c.DiscountID,
                RelatedCategoryIDs = c.RelatedCategoryIDs,
                ProductCount = c.ProductCount,
                MetaTitle = c.MetaTitle,
                MetaDescription = c.MetaDescription,
                Tags = c.Tags,
                LanguageCode = c.LanguageCode,
                CreatedDate = c.CreatedDate,
                ModifiedDate = c.ModifiedDate,
                CreatedBy = c.CreatedBy,
                ModifiedBy = c.ModifiedBy,
                IsDeleted = c.IsDeleted,
                AccessRoles = c.AccessRoles,
                AvailabilitySchedule = c.AvailabilitySchedule,
                ShowInFooter = c.ShowInFooter,
                LayoutType = c.LayoutType,
                ThumbnailImageUrl = c.ThumbnailImageUrl,
                BannerImageUrl = c.BannerImageUrl
            };
        }
    }
}
