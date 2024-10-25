using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PensionFund.API.Contracts.Users;
using PensionFund.Application.Services;
using System.Threading.Tasks;

namespace PensionFund.API.Endpoints
{
    public static class UserEndpoint
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this
            IEndpointRouteBuilder app)
        {
            app.MapPost("Register", Register);
            app.MapPost("Login", Login);
            app.MapPost("Delete", Delete);

            return app;
        }

        private static async Task<IResult> Register(
            [FromBody]RegisterUserRequest request,
            UserService userService)
        {
            await userService.Register(request.Login, request.Password, request.Role);

            return Results.Ok("Register");
        }

        private static async Task<IResult> Login(
            [FromBody]LoginUserRequest request,
            UserService userService,
            HttpContext context)
        {
            var token = await userService.Login(request.Login, request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok();
        }

        private static async Task<IResult> Delete(Guid id, UserService userService, HttpContext context)
        {
            return Results.Ok(await userService.DeleteUser(id));
        }
    }
}
