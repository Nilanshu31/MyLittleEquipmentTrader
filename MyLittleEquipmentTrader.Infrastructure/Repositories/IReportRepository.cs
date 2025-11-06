using MyLittleEquipmentTrader.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Domain.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllAsync();
        IQueryable<Report> GetQueryable();

        // Add a new report
        Task AddAsync(Report report);

        // Get report by ID
        Task<Report> GetByIdAsync(int reportId);

        // Update an existing report
        void Update(Report report);

        // Optional filter method
        Task<IEnumerable<Report>> GetByFilterAsync(string someFilter);
    }
}
