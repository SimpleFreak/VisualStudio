using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PensionFund.API.Contracts.ReportContract;
using PensionFund.Application.Services;
using PensionFund.Core.Models;

namespace PensionFund.API.Endpoints;

public static class ReportEndpoint
{
    public static IEndpointRouteBuilder AddReportEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("GetAllReports", GetAllReports);
        app.MapPost("CreateReport", CreateReport);
        app.MapPut("UpdateReport", UpdateReport);
        app.MapDelete("DeleteReport", DeleteReport);
        
        return app;
    }

    private static async Task<IResult> GetAllReports(ReportService reportService)
    {
        try
        {
            var reports = await reportService.GetAllReports();

            var response = reports.Select(report => new ReportResponse(
                report.Id, report.ReportName, report.ImageReport, report.DateReport));

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> CreateReport([FromBody] ReportRequest request, 
        ReportService reportService)
    {
        try
        {
            var (report, error) = Report.Create(
                Guid.NewGuid(),
                request.ReportName,
                request.ImageReport,
                request.DateReport);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var reportId = await reportService.CreateReport(report);

            return Results.Ok(reportId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> UpdateReport([FromBody] ReportRequest request, Guid id,
        ReportService reportService)
    {
        try
        {
            var response =
                await reportService.UpdateReport(id, request.ReportName, request.ImageReport, request.DateReport);

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> DeleteReport(Guid id, ReportService reportService)
    {
        try
        {
            return Results.Ok(await reportService.DeleteReport(id));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}