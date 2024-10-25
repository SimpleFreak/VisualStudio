using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionFund.Core.Models
{
    public class Prediction
    {
        [Key, Required, NotNull]
        public Guid Id { get; set; }

        [Required, DataType(DataType.DateTime), NotNull]
        public DateTime RetirementDate { get; set; }

        [Required, NotNull]
        public double PredictionCoefficient { get; set; }

        [Required, NotNull]
        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        private Prediction(Guid id, DateTime retirementDate,
            double predictionCoefficient, Guid personId)
        {
            Id = id;
            RetirementDate = retirementDate;
            PredictionCoefficient = predictionCoefficient;
            PersonId = personId; 
        }

        public static (Prediction Prediction, string Error) Create(Guid id,
            DateTime retirementDate, double predictionCoefficient, Guid personId,
            Person person)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(Convert.ToString(retirementDate)))
            {
                error = "RetirementDate cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(predictionCoefficient)))
            {
                error += "\nPredictionCoefficient cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(personId)))
            {
                error += "\nPersonId cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(person)))
            {
                error += "\nPerson cannot be undefined or empty.";
            }

            var prediction = new Prediction(id, retirementDate, predictionCoefficient,
                personId);

            return (prediction, error);
        }
    }
}
