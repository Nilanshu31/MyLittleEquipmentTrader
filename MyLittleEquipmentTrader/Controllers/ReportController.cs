using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLittleEquipmentTrader.Application.Models;
using MyLittleEquipmentTrader.Application.Models.Dtos;
using MyLittleEquipmentTrader.Application.Services;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // GET: /api/Report
    [HttpGet]
    [Authorize(Policy = "CanViewReports")]
    public async Task<IActionResult> GetReports()
    {
        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }

    // POST: /api/Report/filter  
    [HttpPost("filter")]
    [Authorize(Policy = "CanfilterReports")] // You can also create a separate Filter policy if needed
    public async Task<IActionResult> FilterReports([FromBody] ProductFilterRequest request)
    {
        if (request == null)
            return BadRequest("Filter request cannot be null.");

        var pagedReports = await _reportService.GetFilteredReportsAsync(request);
        return Ok(pagedReports);
    }

    // POST: /api/Report
    [HttpPost]
    [Authorize(Policy = "CanCreateReports")] // Create policy (add this to your Authorization policies)
    public async Task<IActionResult> AddReport([FromBody] ReportDto reportDto)
    {
        if (reportDto == null)
            return BadRequest("Report data cannot be null.");

        var createdReport = await _reportService.AddReportAsync(reportDto);
        return Ok(createdReport);
    }

    // DELETE: /api/Report/{id}
    [HttpDelete("{id}")]
    [Authorize(Policy = "CanDeleteReports")] // Delete policy (add this to your Authorization policies)
    public async Task<IActionResult> DeleteReport(int id)
    {
        var result = await _reportService.DeleteReportAsync(id);
        if (!result)
            return NotFound(new { message = "Report not found." });

        return NoContent(); // 204
    }
}
