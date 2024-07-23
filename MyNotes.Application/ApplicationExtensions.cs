using Microsoft.Extensions.DependencyInjection;
using MyNotes.Application.Services;

namespace MyNotes.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<NotesService>();
            services.AddScoped<UserService>();

            return services;
        }
    }
}
