using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionFund.Core.Models
{
    /* Класс человека для дальнейшего представления сущности для базы данных */
    public class Person
    {
        [Key, Required, NotNull]
        public Guid Id { get; set; }

        [Required, NotNull]
        public string FullName { get; set; } = string.Empty;

        [Required, NotNull]
        public int Age { get; set; }

        [Required, NotNull]
        public string Gender { get; set; } = string.Empty;

        [Required, NotNull]
        public decimal Salary { get; set; }

        [Required, NotNull]
        public string WorkExperience { get; set; } = string.Empty;

        [Required, NotNull]
        public string CityResidence { get; set; } = string.Empty;

        public Guid? PredictionId { get; set; }

        public Prediction? Prediction { get; set; }

        private Person(Guid id, string fullName, int age, string gender,
            decimal salary, string workExperience, string cityResidence)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Gender = gender;
            Salary = salary;
            WorkExperience = workExperience;
            CityResidence = cityResidence;
        }

        private Person(Guid id, string fullName, int age, string gender,
            decimal salary, string workExperience, string cityResidence,
            Guid predictionId, Prediction prediction)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Gender = gender;
            Salary = salary;
            WorkExperience = workExperience;
            CityResidence = cityResidence;
            PredictionId = predictionId;
            Prediction = prediction;
        }

        /* Реализация паттерна 'Абстрактный метод для сокрытия реализации' */
        public static (Person Person, string Error) Create(Guid id, string fullName,
            int age, string gender, decimal salary, string workExperience,
            string cityResidence)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(fullName))
            {
                error = "Password cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(age)))
            {
                error += "\nAge cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(gender))
            {
                error += "\nGender cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(salary)))
            {
                error += "\nSalary cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(workExperience))
            {
                error += "\nWorkExperience cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(cityResidence))
            {
                error += "\nCityResidence cannot be undefined or empty.";
            }

            var person = new Person(id, fullName, age, gender, salary,
                workExperience, cityResidence);

            return (person, error);
        }

        public static (Person Person, string Error) Create(Guid id, string fullName,
            int age, string gender, decimal salary, string workExperience,
            string cityResidence, Guid predictionId, Prediction prediction)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(fullName))
            {
                error = "Password cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(age)))
            {
                error += "\nAge cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(gender))
            {
                error += "\nGender cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(salary)))
            {
                error += "\nSalary cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(workExperience))
            {
                error += "\nWorkExperience cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(cityResidence))
            {
                error += "\nCityResidence cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(predictionId)))
            {
                error += "\nPredictionId cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(prediction)))
            {
                error += "\nPrediction cannot be undefined or empty.";
            }

            var person = new Person(id, fullName, age, gender, salary,
                workExperience, cityResidence, predictionId, prediction);

            return (person, error);
        }
    }
}
