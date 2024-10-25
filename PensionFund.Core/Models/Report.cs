using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionFund.Core.Models
{
    public class Report
    {
        [Key, Required, NotNull]
        public Guid Id { get; set; }

        [Required, NotNull]
        public string ReportName { get; set; } = string.Empty;

        [Required, NotNull]
        public byte[] ImageReport { get; set; } = [];

        [Required, DataType(DataType.DateTime), NotNull]
        public DateTime DateReport { get; set; }

        private Report(Guid id, string reportName, byte[] imageReport,
            DateTime dateReport)
        {
            Id = id;
            ReportName = reportName;
            ImageReport = imageReport;
            DateReport = dateReport;
        }

        public static (Report Report, string Error) Create(Guid id, string reportName,
            byte[] imageReport, DateTime dateReport)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(reportName))
            {
                error = "ReportName cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(imageReport)))
            {
                error += "\nImageReport cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(dateReport)))
            {
                error += "\nDateReport cannot be undefined or empty.";
            }

            var report = new Report(id, reportName, imageReport, dateReport);

            return (report, error);
        }
    }
}
