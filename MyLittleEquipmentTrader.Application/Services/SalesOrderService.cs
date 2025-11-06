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
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ✅ Get all sales orders with includes
        public async Task<IEnumerable<SalesOrderDto>> GetAllSalesOrdersAsync()
        {
            var salesOrders = await _unitOfWork.SalesOrders
    .AsQueryable()
    .ToListAsync();


            return salesOrders.Select(MapToDto);
        }

        // ✅ Get filtered, sorted, and paginated sales orders
        public async Task<PagedResult<SalesOrderDto>> GetFilteredSalesOrdersAsync(ProductFilterRequest filterRequest)
        {
            filterRequest ??= new ProductFilterRequest();

            var query = _unitOfWork.SalesOrders
      .AsQueryable();

            // ✅ Apply dynamic filters (e.g., OrderStatus, TenantId, etc.)
            if (filterRequest.Filters != null && filterRequest.Filters.Any())
            {
                query = FilterHelper.ApplyFilters(query, filterRequest.Filters);
            }

            // ✅ Apply sorting dynamically
            query = FilterHelper.ApplySorting(query, filterRequest.SortBy, filterRequest.SortOrder);

            // ✅ Get total record count before pagination
            var totalCount = await query.CountAsync();

            // ✅ Apply pagination (if applicable)
            List<SalesOrder> salesOrders;
            if (filterRequest.PageSize <= 0)
            {
                salesOrders = await query.ToListAsync();
            }
            else
            {
                salesOrders = await query
                    .Skip((filterRequest.Page - 1) * filterRequest.PageSize)
                    .Take(filterRequest.PageSize)
                    .ToListAsync();
            }

            var dtos = salesOrders.Select(MapToDto).ToList();

            // ✅ Build paginated result
            return new PagedResult<SalesOrderDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                CurrentPage = filterRequest.Page,
                PageSize = filterRequest.PageSize <= 0 ? totalCount : filterRequest.PageSize,
                TotalPages = filterRequest.PageSize <= 0
                    ? 1
                    : (int)Math.Ceiling(totalCount / (double)filterRequest.PageSize)
            };
        }

        // ✅ DTO Mapper
        private SalesOrderDto MapToDto(SalesOrder order)
        {
            return new SalesOrderDto
            {
                SalesOrderId = order.SalesOrderId,
                TenantId = order.TenantId,
                CustomerId = order.CustomerId,
                OrderNumber = order.OrderNumber,
                SubtotalAmount = order.SubtotalAmount,
                DiscountAmount = order.DiscountAmount,
                TaxAmount = order.TaxAmount,
                TotalAmount = order.TotalAmount,
                ShippingAmount = order.ShippingAmount,
                PaymentProcessingFee = order.PaymentProcessingFee,
                Currency = order.Currency,
                PaymentMethod = order.PaymentMethod,
                CouponCode = order.CouponCode,
                CouponId = order.CouponId,
                SubscriptionId = order.SubscriptionId,
                FulfillmentDate = order.FulfillmentDate,
                FulfillmentStatus = order.FulfillmentStatus,
                OrderNotes = order.OrderNotes,
                IsGift = order.IsGift,
                IsArchived = order.IsArchived,
                IsDeleted = order.IsDeleted,
                ShippingAddress = order.ShippingAddress,
                ShippingMethod = order.ShippingMethod,
                ShippingTrackingNumber = order.ShippingTrackingNumber,
                AffiliateId = order.AffiliateId,
                Device = order.Device,
                IsPaid = order.IsPaid,
                PaidDate = order.PaidDate,
                IsRefunded = order.IsRefunded,
                RefundDate = order.RefundDate,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt
            };
        }
    }
}
