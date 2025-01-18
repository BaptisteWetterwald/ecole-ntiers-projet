using Microsoft.EntityFrameworkCore;
using Blaze4.Application.Models;

namespace Blaze4Backend.Data;

public class Blaze4DbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Cell> Cells { get; set; }

    public Blaze4DbContext(DbContextOptions<Blaze4DbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration des entités

        // Player
        modelBuilder.Entity<Player>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Player>()
            .HasIndex(p => p.Username)
            .IsUnique(); // Username doit être unique

        // Game
        modelBuilder.Entity<Game>()
            .HasKey(g => g.Id);
        modelBuilder.Entity<Game>()
            .HasOne(g => g.Host)
            .WithMany(p => p.Games)
            .HasForeignKey(g => g.HostId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Game>()
            .HasOne(g => g.Guest)
            .WithMany()
            .HasForeignKey(g => g.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Cell
        modelBuilder.Entity<Cell>()
            .HasKey(c => new { c.Row, c.Column, c.GameId }); // Clé composite
    }
}