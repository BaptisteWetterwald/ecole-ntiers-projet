using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess;

// Add sample data to the database
public static class DbInitializer
{
    public static void Initialize(Puissance4DbContext context)
    {
        context.Database.EnsureCreated();

        // Look for any players.
        if (context.Players.Any())
        {
            return;   // DB has been seeded
        }

        var players = new Player[]
        {
            new(){Username= "Alice"},
            new(){Username= "Bob"},
            new(){Username= "Charlie"},
            new(){Username = "David"},
        };
        foreach (var p in players)
        {
            context.Players.Add(p);
        }


        var games = new Game[]
        {
            new(){State="Empty",CreatedAt=DateTime.UtcNow},
            new(){State="Empty",CreatedAt=DateTime.UtcNow},
        };
        foreach (var g in games)
        {
            context.Games.Add(g);
        }

        var tokens = new TokenEntity[]
        {
            new(){Color="Red"},
            new(){Color="Yellow"},
            new(){Color="Red"},
        };
        foreach (var t in tokens)
        {
            context.Tokens.Add(t);
        }
        
        context.SaveChanges();
    }
}