using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionFund.Core.Models;

namespace PensionFund.DataAccess.Configurations
{
    public class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.HasKey(prediction => prediction.Id);

            builder.Property(prediction => prediction.RetirementDate)
                .IsRequired();

            builder.Property(prediction => prediction.PredictionCoefficient)
                .IsRequired();

            builder.Property(prediction => prediction.PersonId)
                .IsRequired();

            builder.Property(prediction => prediction.Person)
                .IsRequired();
        }
    }
}
