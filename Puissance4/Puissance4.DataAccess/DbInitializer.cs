using System.Security.Cryptography;
using System.Text;
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
        
        var players = new[]
        {
            new EFPlayer { Login = "Baptouste", PasswordHash = HashPassword("#qlflop") },
            new EFPlayer { Login = "Mehmett", PasswordHash = HashPassword("ChefMehmett") },
            new EFPlayer { Login = "Kepplouf", PasswordHash = HashPassword("Thomsoja") }
        };
        context.Players.AddRange(players);
        
        context.SaveChanges();
    }
    
    private static string HashPassword(string password)
    {
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }
}