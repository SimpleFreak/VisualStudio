using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Interfaces
{
    public interface IReportService
    {
        Task<Guid> CreateReport(Report report);
        
        Task<Guid> DeleteReport(Guid id);
        
        Task<List<Report>> GetAllReports();
        
        Task<Guid> UpdateReport(Guid id, string reportName, byte[] imageReport, DateTime dateReport);
    }
}