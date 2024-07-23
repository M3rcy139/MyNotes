using MyNotes.Application.Services;
using MyNotes.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace MyNotes.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);

            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(
            [FromBody]RegisterUserRequest request,
            UserService userService)
        {
            await userService.Register(request.UserName,request.Password, request.Email, request.Role);

            return Results.Ok();
        }

        private static async Task<IResult> Login([FromBody]LoginUserRequest request, 
            UserService usersService, HttpContext context)
        {
            var token = await usersService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("tasty-cookies", token);

            return Results.Ok(token);
        }
    }
}
