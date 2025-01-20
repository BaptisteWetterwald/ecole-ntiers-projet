using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess;

// Add sample data to the database
public static class DbInitializer
{
    public static void Initialize(Puissance4DbContext context)
    {
        context.Database.EnsureCreated();

        // Look for any players.
        if (context.Tokens.Any())
        {
            return;   // DB has been seeded
        }
        
        // Add samples
        
        
        context.SaveChanges();
    }
}