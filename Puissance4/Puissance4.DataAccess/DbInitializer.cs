namespace Puissance4.DataAccess;

// Add sample data to the database
public static class DbInitializer
{
    public static void Initialize(GameContext context)
    {
        context.Database.EnsureCreated();

        // Look for any players.
        if (context.Players.Any())
        {
            return;   // DB has been seeded
        }

        var players = new Player[]
        {
            new Player{Username="Alice"},
            new Player{Username="Bob"},
            new Player{Username="Charlie"},
            new Player{Username="David"},
            new Player{Username="Eve"},
            new Player{Username="Frank"},
            new Player{Username="Grace"},
            new Player{Username="Heidi"},
            new Player{Username="Ivan"},
            new Player{Username="Judy"},
            new Player{Username="Mallory"},
            new Player{Username="Oscar"},
            new Player{Username="Peggy"},
            new Player{Username="Sybil"},
            new Player{Username="Trent"},
            new Player{Username="Walter"}
        };
        foreach (Player p in players)
        {
            context.Players.Add(p);
        }


        var games = new Game[]
        {
            new Game(){State="Empty",CreatedAt=DateTime.UtcNow},
            new Game(){State="Empty",CreatedAt=DateTime.UtcNow},
        };
        foreach (Game g in games)
        {
            context.Games.Add(g);
        }

        context.SaveChanges();
    }
}