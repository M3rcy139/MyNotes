using MyNotes.Application.Interfaces.Auth;
using MyNotes.Application.Services;
using MyNotes.Infrastructure;
using MyNotes.Application.Interfaces.Repositories;
using MyNotes.Persistence.Repositories;
using MyNotes.Persistence;
using MyNotes.Extensions;
using Microsoft.EntityFrameworkCore;
using MyNotes.Middlewares;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApiAuthentication(configuration);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTransient<ExceptionMiddleware>();

services.AddDbContext<NotesDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(NotesDbContext)));
});
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.AddScoped<INotesRepository, NotesRepository>();
services.AddScoped<IUsersRepository, UsersRepository>();

services.AddScoped<NotesService>();
services.AddScoped<UsersService>();

services.AddAutoMapper(typeof(DataBaseMappings));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
});

app.UseAuthentication();
app.UseAuthorization();

app.AddMappedEndpoints();

app.MapGet("get", () =>
{
    return Results.Ok("ok");
}).RequireAuthorization();

app.Run();
