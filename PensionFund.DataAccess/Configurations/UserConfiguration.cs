using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionFund.Core.Models;

namespace PensionFund.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Login)
                .IsRequired();

            builder.Property(user => user.Password)
                .IsRequired();

            builder.Property(user => user.Role)
                .IsRequired();
        }
    }
}
