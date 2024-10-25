using System;

namespace PensionFund.API.Contracts.ReportContract
{
    public record ReportResponse(
        Guid Id,
        string ReportName,
        byte[] ImageReport,
        DateTime DateReport
    );
}
