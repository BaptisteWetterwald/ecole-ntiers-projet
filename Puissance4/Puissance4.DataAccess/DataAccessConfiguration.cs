using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Puissance4.DataAccess;

public static class DataAccessConfiguration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GameContext>(options =>
            options.UseSqlite(connectionString)); // Utilise SQLite comme base de données
        return services;
    }
}