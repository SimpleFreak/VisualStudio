using Microsoft.EntityFrameworkCore;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.DataAccess.Repositories
{
    public class ReportRepository(PensionFundDbContext context) : IReportRepository
    {
        private readonly PensionFundDbContext _context = context;

        public async Task<List<Report>> Get()
        {
            var reportEntities = await _context.Reports
                .AsNoTracking()
                .ToListAsync();

            var reports = reportEntities
                .Select(report => Report
                    .Create(report.Id, report.ReportName, report.ImageReport, report.DateReport).Report)
                .ToList();

            return reports;
        }

        public async Task<Guid> Create(Report report)
        {
            var reportEntity = Report.Create(report.Id, report.ReportName,
                report.ImageReport, report.DateReport);

            await _context.Reports.AddAsync(reportEntity.Report);
            await _context.SaveChangesAsync();

            return reportEntity.Report.Id;
        }

        public async Task<Guid> Update(Guid id, string reportName, byte[] imageReport,
            DateTime dateReport)
        {
            await _context.Reports
                .Where(report => report.Id == id)
                .ExecuteUpdateAsync(set => set
                .SetProperty(report => report.ReportName, user => reportName)
                .SetProperty(report => report.ImageReport, report => imageReport)
                .SetProperty(report => report.DateReport, report => dateReport));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Reports
                .Where(report => report.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
