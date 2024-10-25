using System;

namespace PensionFund.API.Contracts.ReportContract
{
    public record ReportRequest(
        string ReportName,
        byte[] ImageReport,
        DateTime DateReport
    );
}
