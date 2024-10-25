using PensionFund.Application.Interfaces;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Services
{
    public class ReportService(IReportRepository reportRepository) : IReportService
    {
        private readonly IReportRepository _reportRepository = reportRepository;

        public async Task<List<Report>> GetAllReports()
        {
            return await _reportRepository.Get();
        }

        public async Task<Guid> CreateReport(Report report)
        {
            return await _reportRepository.Create(report);
        }

        public async Task<Guid> UpdateReport(Guid id, string reportName,
            byte[] imageReport, DateTime dateReport)
        {
            return await _reportRepository.Update(id, reportName, imageReport,
                dateReport);
        }

        public async Task<Guid> DeleteReport(Guid id)
        {
            return await _reportRepository.Delete(id);
        }
    }
}
