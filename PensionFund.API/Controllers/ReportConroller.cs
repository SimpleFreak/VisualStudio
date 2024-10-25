using Microsoft.AspNetCore.Mvc;
using PensionFund.API.Contracts.ReportContract;
using PensionFund.API.Contracts.UserContract;
using PensionFund.Application.Interfaces;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.API.Controllers
{
    [ApiController, Route("[controller]")]
    public class ReportController(IReportService reportService)
        : ControllerBase
    {
        private readonly IReportService _reportService = reportService;

        [HttpGet("GetReports")]
        public async Task<ActionResult<List<ReportResponse>>> GetReports()
        {
            var reports = await _reportService.GetAllReports();

            var response = reports.Select(report => new ReportResponse(report.Id, report.ReportName, report.ImageReport, report.DateReport));

            return Ok(response);
        }

        [HttpPost("CreateReport")]
        public async Task<ActionResult<Guid>> CreateReport(
            [FromBody] ReportRequest request)
        {
            var (report, error) = Report.Create(
                Guid.NewGuid(),
                request.ReportName,
                request.ImageReport,
                request.DateReport);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var reportId = await _reportService.CreateReport(report);

            return Ok(reportId);
        }

        [HttpPut("UpdateReport {id:guid}")]
        public async Task<ActionResult<Guid>> UpdateReport(Guid id,
            [FromBody] ReportRequest request)
        {
            return Ok(await _reportService.UpdateReport(id, request.ReportName, request.ImageReport, request.DateReport));
        }

        [HttpDelete("DeleteReport{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteReport(Guid id)
        {
            return Ok(await _reportService.DeleteReport(id));
        }
    }
}
