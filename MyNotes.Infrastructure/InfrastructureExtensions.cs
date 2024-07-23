using Microsoft.Extensions.DependencyInjection;
using MyNotes.Application.Interfaces.Auth;
using MyNotes.Infrastructure.Authentication;

namespace MyNotes.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
