using Microsoft.AspNetCore.Mvc;
using PensionFund.API.Contracts.PersonContract;
using PensionFund.API.Contracts.PredictionContract;
using PensionFund.Application.Interfaces;
using PensionFund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionFund.API.Controllers
{
    [ApiController, Route("[controller]")]
    public class PredictionController(IPredictionService predictionService)
        : ControllerBase
    {
        private readonly IPredictionService _predictionService = predictionService;

        [HttpGet("GetAllPredictions")]
        public async Task<ActionResult<List<PredictionResponse>>> GetPredictions()
        {
            var predictions = await _predictionService.GetAllPredictions();

            var response = predictions.Select(prediction => new PredictionResponse(
                prediction.Id, prediction.RetirementDate,
                prediction.PredictionCoefficient, prediction.PersonId,
                prediction.Person));

            return Ok(response);
        }

        [HttpPost("CreatePrediction")]
        public async Task<ActionResult<Guid>> CreatePrediction(
            [FromBody] PredictionRequest request)
        {
            var (prediction, error) = Prediction.Create(
                Guid.NewGuid(),
                request.RetirementDate,
                request.PredictionCoefficient,
                request.PersonId,
                request.Person);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var predictionId = await _predictionService.CreatePrediction(prediction);

            return Ok(predictionId);
        }

        [HttpPut("UpdatePrediction {id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePrediction(Guid id,
            [FromBody] PredictionRequest request)
        {
            return Ok(await _predictionService.UpdatePrediction(id,
                request.RetirementDate, request.PredictionCoefficient,
                request.PersonId, request.Person)); ;
        }

        [HttpDelete("DeletePrediction {id:guid}")]
        public async Task<ActionResult<Guid>> DeletePrediction(Guid id)
        {
            return Ok(await _predictionService.DeletePrediction(id));
        }
    }
}
