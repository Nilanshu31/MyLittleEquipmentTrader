using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Application.Services
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> GetAllReportsAsync();
        Task<PagedResult<ReportDto>> GetFilteredReportsAsync(ProductFilterRequest filterRequest);

        // Add other CRUD methods here if needed
        // Task<ReportDto> GetReportByIdAsync(int reportId);
        // Task<ReportDto> CreateReportAsync(ReportDto reportCreateDto);
        // Task<ReportDto> UpdateReportAsync(int reportId, ReportUpdateDto reportUpdateDto);
        // Task<bool> DeleteReportAsync(int reportId);
        Task<ReportDto> AddReportAsync(ReportDto dto);
        Task<bool> DeleteReportAsync(int reportId);

    }
}
