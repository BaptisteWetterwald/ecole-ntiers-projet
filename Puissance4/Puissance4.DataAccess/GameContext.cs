using Microsoft.EntityFrameworkCore;

namespace Puissance4.DataAccess
{
    public class GameContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=puissance4.db");
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}