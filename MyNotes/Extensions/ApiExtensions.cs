using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyNotes.Endpoints;
using System.Text;
using MyNotes.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Security;

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
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

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

                    options.TokenValidationParameters = new()
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


            services.AddAuthorization();
        }
    }
}
