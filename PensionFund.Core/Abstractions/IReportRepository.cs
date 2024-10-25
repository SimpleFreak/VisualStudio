using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Core.Abstractions
{
    public interface IReportRepository
    {
        Task<List<Report>> Get();
        Task<Guid> Create(Report report);
        Task<Guid> Update(Guid id, string reportName, byte[] imageReport, DateTime dateReport);
        Task<Guid> Delete(Guid id);
    }
}
