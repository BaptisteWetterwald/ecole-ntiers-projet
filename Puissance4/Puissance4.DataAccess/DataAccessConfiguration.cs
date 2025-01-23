using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Puissance4.DataAccess.Repositories.Implementations;
using Puissance4.DataAccess.Repositories.Interfaces;

namespace Puissance4.DataAccess;

public static class DataAccessConfiguration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<Puissance4DbContext>(options =>
                options.UseSqlite(connectionString)
            //.EnableSensitiveDataLogging()
            //.LogTo(Console.WriteLine)
        ); // Utilise SQLite comme base de données

        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<IGridRepository, GridRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();

        return services;
    }
}