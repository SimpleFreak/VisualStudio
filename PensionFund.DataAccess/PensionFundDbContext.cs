using Microsoft.EntityFrameworkCore;
using PensionFund.Core.Models;

namespace PensionFund.DataAccess
{
    /* Класс контекста базы данных */
    public class PensionFundDbContext(DbContextOptions<PensionFundDbContext> options)
        : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Prediction> Predictions { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prediction>()
                .HasOne(prediction => prediction.Person)
                .WithOne(person => person.Prediction)
                .HasForeignKey<Prediction>(pred => pred.PersonId);
        }
    }
}
