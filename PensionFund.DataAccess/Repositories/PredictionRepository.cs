using Microsoft.EntityFrameworkCore;
using PensionFund.Core.Abstractions;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.DataAccess.Repositories
{
    public class PredictionRepository(PensionFundDbContext context) : IPredictionRepository
    {
        private readonly PensionFundDbContext _context = context;

        public async Task<List<Prediction>> Get()
        {
            var predictionEntities = await _context.Predictions
                .AsNoTracking()
                .ToListAsync();

            var predictions = predictionEntities
                .Select(prediction => Prediction
                    .Create(prediction.Id, prediction.RetirementDate,
                    prediction.PredictionCoefficient, prediction.PersonId, prediction.Person).Prediction)
                .ToList();

            return predictions;
        }

        public async Task<Guid> Create(Prediction prediction)
        {
            var predictionEntity = Prediction.Create(prediction.Id,
                prediction.RetirementDate, prediction.PredictionCoefficient,
                prediction.PersonId, prediction.Person);

            await _context.Predictions.AddAsync(predictionEntity.Prediction);
            await _context.SaveChangesAsync();

            return predictionEntity.Prediction.Id;
        }

        public async Task<Guid> Update(Guid id, DateTime retirementDate,
            double predictionCoefficient, Guid personId, Person person)
        {
            await _context.Predictions
                .Where(prediction => prediction.Id == id)
                .ExecuteUpdateAsync(set => set
                .SetProperty(prediction => prediction.RetirementDate,
                    prediction => retirementDate)
                .SetProperty(prediction => prediction.PredictionCoefficient,
                    prediction => predictionCoefficient)
                .SetProperty(prediction => prediction.PersonId,
                    prediction => personId)
                .SetProperty(prediction => prediction.Person,
                    prediction => person));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Predictions
                .Where(prediction => prediction.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
