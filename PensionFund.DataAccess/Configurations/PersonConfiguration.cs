using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionFund.Core.Models;

namespace PensionFund.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        /* Конфигурация сущности Человека */
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(person => person.Id);

            builder.Property(person => person.FullName)
                .IsRequired();

            builder.Property(person => person.Age)
                .IsRequired();

            builder.Property(person => person.Gender)
                .IsRequired();

            builder.Property(person => person.Salary)
                .IsRequired();

            builder.Property(person => person.WorkExperience)
                .IsRequired();

            builder.Property(person => person.CityResidence)
                .IsRequired();
        }
    }
}
