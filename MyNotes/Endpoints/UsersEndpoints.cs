using MyNotes.Application.Interfaces.Services;
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
            IUserService userService)
        {
            try
            {
                await userService.Register(request.UserName,request.Password, request.Email, request.Role);

                return Results.Ok();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                var problemDetails = new
                {
                    error = "An unexpected error occurred.",
                    details = ex.Message
                };

                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        }

        private static async Task<IResult> Login([FromBody]LoginUserRequest request, 
            IUserService usersService, HttpContext context)
        {
            try
            {
                var token = await usersService.Login(request.Email, request.Password);

                context.Response.Cookies.Append("tasty-cookies", token);

                return Results.Ok(token);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Json(new { error = ex.Message }, statusCode: 401);
            }
            catch (Exception ex)
            {
                var problemDetails = new
                {
                    error = "An unexpected error occurred.",
                    details = ex.Message
                };

                return Results.Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
