using PensionFund.Application.Interfaces;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Application.Services
{
    public class PredictionService(IPredictionRepository predictionRepository) : IPredictionService
    {
        private readonly IPredictionRepository _predictionRepository = predictionRepository;

        public async Task<List<Prediction>> GetAllPredictions()
        {
            return await _predictionRepository.Get();
        }

        public async Task<Guid> CreatePrediction(Prediction prediction)
        {
            return await _predictionRepository.Create(prediction);
        }

        public async Task<Guid> UpdatePrediction(Guid id, DateTime retirementDate,
            double predictionCoefficient, Guid personId, Person person)
        {
            return await _predictionRepository.Update(id, retirementDate,
                predictionCoefficient, personId, person);
        }

        public async Task<Guid> DeletePrediction(Guid id)
        {
            return await _predictionRepository.Delete(id);
        }
    }
}
