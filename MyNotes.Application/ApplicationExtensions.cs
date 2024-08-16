using Microsoft.Extensions.DependencyInjection;
using MyNotes.Application.Services;
using MyNotes.Application.Interfaces.Services;

namespace MyNotes.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
