using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Core.Interfaces.Repositories;
using MyNotes.Persistence.Repositories;

namespace MyNotes.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(NotesDbContext)));
            });

            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
