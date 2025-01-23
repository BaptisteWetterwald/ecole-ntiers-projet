using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Puissance4.DataAccess;

public class Puissance4DbContextFactory : IDesignTimeDbContextFactory<Puissance4DbContext>
{
    public Puissance4DbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Puissance4DbContext>();
        optionsBuilder.UseSqlite("Data Source=./puissance4.db"); // Remplacez par votre chaîne de connexion

        return new Puissance4DbContext(optionsBuilder.Options);
    }
}