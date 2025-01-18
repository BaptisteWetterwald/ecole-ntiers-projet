using Blaze4.Application.Models;

namespace Blaze4Backend.Data;

// Initializes the database with test data
public class DbInitializer
{
    public static void Initialize(Blaze4DbContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context.Players.Any())
        {
            return; // DB has been seeded
        }

        var players = new Player[]
        {
            new Player { Login = "Baptiste", Password = "bapt123" },
            new Player { Login = "Gauthier", Password = "gauth123" },
            new Player { Login = "Thomas", Password = "thomas123" }
        };
        
        foreach (Player p in players)
        {
            context.Players.Add(p);
        }
        context.SaveChanges();
    }
}