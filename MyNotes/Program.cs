using MyNotes.Application;
using MyNotes.Persistence;
using MyNotes.Persistence.Mappings;
using MyNotes.Extensions;
using MyNotes.Middlewares;
using Microsoft.AspNetCore.CookiePolicy;
using MyNotes.Infrastructure.Authentication;
using MyNotes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApiAuthentication(configuration);

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

services.AddTransient<ExceptionMiddleware>();

services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure();

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

app.Run();
