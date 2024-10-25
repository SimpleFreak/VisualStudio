using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionFund.Core.Models;

namespace PensionFund.DataAccess.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(report => report.Id);

            builder.Property(report => report.ReportName)
                .IsRequired();

            builder.Property(report => report.ImageReport)
                .IsRequired();

            builder.Property(report => report.DateReport)
                .IsRequired();
        }
    }
}
