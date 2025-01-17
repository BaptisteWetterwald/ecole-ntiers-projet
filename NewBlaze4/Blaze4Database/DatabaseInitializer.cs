using Blaze4Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blaze4Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize(Blaze4DbContext context)
        {
            context.Database.Migrate();

            // Ajout d'exemple de joueurs ou de données
            if (!context.Players.Any())
            {
                context.Players.AddRange(
                    new Player("HostPlayer", "password"),
                    new Player("GuestPlayer", "password")
                );
                context.SaveChanges();
            }
        }
    }
}
