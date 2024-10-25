using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionFund.Core.Abstractions
{
    public interface IPredictionRepository
    {
        Task<List<Prediction>> Get();
        Task<Guid> Create(Prediction prediction);
        Task<Guid> Update(Guid id, DateTime retirementDate, double predictionCoefficient, Guid personId, Person person);
        Task<Guid> Delete(Guid id);
    }
}