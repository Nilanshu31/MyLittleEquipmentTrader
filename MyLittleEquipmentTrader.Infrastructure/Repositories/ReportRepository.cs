using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Domain.Entities;
using MyLittleEquipmentTrader.Domain.Repositories;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllAsync()
        {
            return await _context.Report.ToListAsync();
        }

        public IQueryable<Report> GetQueryable()
        {
            return _context.Report.AsQueryable();
        }

        public async Task AddAsync(Report report)
        {
            await _context.Report.AddAsync(report);
        }

        public async Task<Report> GetByIdAsync(int reportId)
        {
            return await _context.Report.FirstOrDefaultAsync(r => r.ReportID == reportId);
        }

        public void Update(Report report)
        {
            _context.Report.Update(report);
        }

        public async Task<IEnumerable<Report>> GetByFilterAsync(string someFilter)
        {
            return await _context.Report
                                 .Where(r => r.ReportName.Contains(someFilter))
                                 .ToListAsync();
        }
    }
}
