using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PensionFund.API.Contracts.PredictionContract;
using PensionFund.Application.Services;
using PensionFund.Core.Models;

namespace PensionFund.API.Endpoints;

public static class PredictionEndpoint
{
    public static IEndpointRouteBuilder AddPredictionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("GetAllPredictions", GetAllPredictions);
        app.MapPost("CreatePrediction", CreatePrediction);
        app.MapPost("UpdatePrediction", UpdatePrediction);
        app.MapPost("DeletePrediction", DeletePrediction);
        return app;
    }

    private static async Task<IResult> GetAllPredictions(PredictionService predictionService)
    {
        try
        {
            var predictions = await predictionService.GetAllPredictions();

            var response = predictions.Select(prediction => new PredictionResponse(
                prediction.Id, prediction.RetirementDate,
                prediction.PredictionCoefficient, prediction.PersonId,
                prediction.Person));

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> CreatePrediction([FromBody] PredictionRequest request, 
        PredictionService predictionService)
    {
        try
        {
            var (prediction, error) = Prediction.Create(
                Guid.NewGuid(),
                request.RetirementDate,
                request.PredictionCoefficient,
                request.PersonId,
                request.Person);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var predictionId = await predictionService.CreatePrediction(prediction);

            return Results.Ok(predictionId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> UpdatePrediction([FromBody] PredictionRequest request, Guid id,
        PredictionService predictionService)
    {
        try
        {
            var response = await predictionService.UpdatePrediction(id,
                request.RetirementDate, request.PredictionCoefficient,
                request.PersonId, request.Person);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> DeletePrediction(Guid id, PredictionService predictionService)
    {
        try
        {
            return Results.Ok(await predictionService.DeletePrediction(id));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}