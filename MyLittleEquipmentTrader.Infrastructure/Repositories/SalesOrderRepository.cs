using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Domain.Repositories;
using MyLittleEquipmentTrader.Infrastructure;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all SalesOrders with eager-loaded related entities
        public async Task<IEnumerable<SalesOrder>> GetAllAsync()
        {
            try
            {
                return await _context.SalesOrders
                    //.Include(o => o.Customer)   // Eager load Customer
                    .Include(o => o.Product)    // Eager load Product
                    .Include(o => o.Tenant)     // Eager load Tenant
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // You can log the exception here
                throw new InvalidOperationException("Error retrieving sales orders.", ex);
            }
        }

        // Get SalesOrders as IQueryable for dynamic querying
        public IQueryable<SalesOrder> AsQueryable()
        {
            return _context.SalesOrders
                //.Include(o => o.Customer)
                .Include(o => o.Product)
                .Include(o => o.Tenant)
                .AsQueryable();
        }

        // Example of filter method (this can be extended further based on needs)
        public async Task<IEnumerable<SalesOrder>> GetByFilterAsync(
            int? tenantId = null,
            int? customerId = null,
            string orderStatus = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = _context.SalesOrders.AsQueryable();

            // Apply filters dynamically
            if (tenantId.HasValue)
                query = query.Where(o => o.TenantId == tenantId);

            if (customerId.HasValue)
                query = query.Where(o => o.CustomerId == customerId);

            if (!string.IsNullOrEmpty(orderStatus))
                query = query.Where(o => o.FulfillmentStatus.Contains(orderStatus));

            if (startDate.HasValue)
                query = query.Where(o => o.OrderDate >= startDate);

            if (endDate.HasValue)
                query = query.Where(o => o.OrderDate <= endDate);

            try
            {
                return await query
                    //.Include(o => o.Customer)  // Eager load Customer
                    .Include(o => o.Product)   // Eager load Product
                    .Include(o => o.Tenant)    // Eager load Tenant
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw new InvalidOperationException("Error filtering sales orders.", ex);
            }
        }

        // Get a specific SalesOrder by its ID
        public async Task<SalesOrder> GetByIdAsync(int salesOrderId)
        {
            return await _context.SalesOrders
                //.Include(o => o.Customer)
                .Include(o => o.Product)
                .Include(o => o.Tenant)
                .FirstOrDefaultAsync(o => o.SalesOrderId == salesOrderId);
        }

        public Task<IEnumerable<SalesOrder>> GetByFilterAsync(string orderNumber = null, int? productId = null, decimal? minAmount = null, decimal? maxAmount = null)
        {
            throw new NotImplementedException();
        }
    }
}
