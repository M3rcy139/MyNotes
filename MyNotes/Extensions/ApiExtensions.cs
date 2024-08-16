using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyNotes.Endpoints;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyNotes.Core.Enums;
using MyNotes.Infrastructure.Authentication;
using MyNotes.Application.Interfaces.Services;
using MyNotes.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace MyNotes.Extensions
{
    public static class ApiExtensions
    {
        public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
        {  
            app.MapNotesEndpoints();
            app.MapUsersEndpoints();
        }

        public static void AddApiAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                        };

                        options.Events = new JwtBearerEvents()
                        {
                            OnMessageReceived = context =>
                            {
                                context.Token = context.Request.Cookies["tasty-cookies"];

                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization();
        }

        public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
            this TBuilder builder, params Permission[] permissions)
            where TBuilder : IEndpointConventionBuilder
        {
            return builder.RequireAuthorization(policy =>
                policy.AddRequirements(new PermissionRequirement(permissions)));
        }

    }
}
