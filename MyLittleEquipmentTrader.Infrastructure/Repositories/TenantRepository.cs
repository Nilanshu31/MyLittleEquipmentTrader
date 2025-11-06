using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System;
using System.Linq;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        private readonly ApplicationDbContext _context;

        public TenantRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Tenant> GetQueryable()
        {
            return _context.Tenants.AsQueryable();
        }
    }
}
