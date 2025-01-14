using Blaze4.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Blaze4Backend.Data;

// This class is the database context for the Blaze4 application. Configures the entities for SQLite
// DbSet<Player> Players, DbSet<Game> Game
public class Blaze4DbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }

    public Blaze4DbContext(DbContextOptions<Blaze4DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>().HasKey(p => p.Login); ;
    }
}