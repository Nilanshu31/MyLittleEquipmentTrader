using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Application.Helpers;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            // Eager load ProductAttributes and ProductTags
            var products = await _unitOfWork.Products
                .GetQueryableWithIncludes(p => p.ProductAttributes, p => p.ProductTags)
                .ToListAsync();

            return products.Select(MapToDto);
        }

        public async Task<PagedResult<ProductDto>> GetFilteredProductsAsync(ProductFilterRequest filterRequest)
        {
            filterRequest ??= new ProductFilterRequest();

            var query = _unitOfWork.Products
                .GetQueryableWithIncludes(p => p.ProductAttributes, p => p.ProductTags);

            if (filterRequest.Filters != null && filterRequest.Filters.Any())
            {
                query = FilterHelper.ApplyFilters(query, filterRequest.Filters);
            }

            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            var totalCount = await query.CountAsync();

            List<Product> products;
            if (filterRequest.PageSize <= 0)
            {
                products = await query.ToListAsync();
            }
            else
            {
                products = await query
                    .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                    .Take(filterRequest.PageSize)
                    .ToListAsync();
            }

            var productDtos = products.Select(MapToDto).ToList();

            return new PagedResult<ProductDto>
            {
                TotalCount = totalCount,
                Items = productDtos,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize <= 0 ? totalCount : filterRequest.PageSize,
                TotalPages = filterRequest.PageSize <= 0
                    ? 1
                    : (int)System.Math.Ceiling(totalCount / (double)filterRequest.PageSize)
            };
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductType = product.ProductType,
                Price = product.Price,
                SalePrice = product.SalePrice,
                SKU = product.SKU,
                Slug = product.Slug,
                ThumbnailURL = product.ThumbnailURL,
                ProductAttributes = product.ProductAttributes?.Select(a => new ProductAttributeDto
                {
                    AttributeName = a.AttributeName,
                    AttributeValue = a.AttributeValue,
                    UnitOfMeasurement = a.UnitOfMeasurement,
                    AttributeGroup = a.AttributeGroup
                }).ToList(),
                ProductTags = product.ProductTags?.Select(t => new ProductTagDto
                {
                    TagID = t.TagID,
                    TagName = t.TagName,
                    Description = t.Description,
                    CategoryID = t.CategoryID,
                    Subcategory = t.Subcategory,
                    TagIcon = t.TagIcon,
                    TagColor = t.TagColor,
                    CreatedDate = t.CreatedDate,
                    LastUpdated = t.LastUpdated,
                    IsActive = t.IsActive,
                    Priority = t.Priority,
                    UserCount = t.UserCount,
                    TagType = t.TagType,
                    Visibility = t.Visibility
                }).ToList()
            };
        }
    }
}
