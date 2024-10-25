using PensionFund.Core.Models;
using System;

namespace PensionFund.API.Contracts.PredictionContract
{
    public record PredictionRequest(
        DateTime RetirementDate,
        double PredictionCoefficient,
        Guid PersonId,
        Person Person
    );
}
