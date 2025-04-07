using MethodTeams.Data;
using MethodTeams.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MethodTeams.Extensions
{
    // Расширения для регистрации сервисов
    public static class TeamManagementExtensions
    {
        public static IServiceCollection AddTeamManagement(this IServiceCollection services,
            Action<DbContextOptionsBuilder> dbContextOptionsAction)
        {
            // Регистрация контекста базы данных
            services.AddDbContext<TeamDbContext>(dbContextOptionsAction);

            // Регистрация сервиса команд
            services.AddScoped<TeamService>();

            return services;
        }
    }
}
