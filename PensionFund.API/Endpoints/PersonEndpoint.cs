using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PensionFund.API.Contracts.PersonContract;
using PensionFund.Application.Services;
using PensionFund.Core.Models;

namespace PensionFund.API.Endpoints;

public static class PersonEndpoint
{
    public static IEndpointRouteBuilder AddPersonEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("GetPersons", GetPersons);
        app.MapPost("CreatePerson", CreatePerson);
        app.MapPost("UpdatePerson", UpdatePerson);
        app.MapPost("DeletePerson", DeletePerson);
        return app;
    }

    private static async Task<IResult> GetPersons(PersonService _personService)
    {
        try
        {
            var persons = await _personService.GetAllPersons();

            var response = persons.Select(person => new PersonResponse(
                person.Id, person.FullName, person.Age, person.Gender, person.Salary));

            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> CreatePerson([FromBody] PersonRequest request, 
        PersonService _personService)
    {
        try
        {
            var (person, error) = Person.Create(
                Guid.NewGuid(),
                request.FullName,
                request.Age,
                request.Gender,
                request.Salary,
                request.WorkExperience,
                request.CityResidence);

            if (!string.IsNullOrEmpty(error))
            {
                return Results.BadRequest(error);
            }

            var personId = await _personService.CreatePerson(person);

            return Results.Ok(personId);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> UpdatePerson([FromBody] PersonRequest request, Guid id,
        PersonService _personService)
    {
        return Results.Ok(await _personService.UpdatePerson(id, request.FullName,
            request.Age, request.Gender, request.Salary, request.WorkExperience,
            request.CityResidence));
    }

    private static async Task<IResult> DeletePerson(Guid id, PersonService _personService)
    {
        return Results.Ok(await _personService.DeletePerson(id));
    }
}