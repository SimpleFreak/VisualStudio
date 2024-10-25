using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Interfaces
{
    public interface IPredictionService
    {
        Task<Guid> CreatePrediction(Prediction prediction);

        Task<Guid> DeletePrediction(Guid id);
        
        Task<List<Prediction>> GetAllPredictions();
        
        Task<Guid> UpdatePrediction(Guid id, DateTime retirementDate, double predictionCoefficient, Guid personId, Person person);
    }
}