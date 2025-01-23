using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess;

public class Puissance4DbContext : DbContext
{
    public Puissance4DbContext(DbContextOptions<Puissance4DbContext> options) : base(options)
    {
    }

    public DbSet<EFPlayer> Players { get; set; }
    public DbSet<EFGame> Games { get; set; }
    public DbSet<EFGrid> Grids { get; set; }
    public DbSet<EFCell> Cells { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Player - Host relation
        modelBuilder.Entity<EFGame>()
            .HasOne(g => g.Host)
            .WithMany(p => p.GamesAsHost) // Relie à GamesAsHost
            .HasForeignKey(g => g.HostId)
            .OnDelete(DeleteBehavior.Restrict);

        // Player - Guest relation
        modelBuilder.Entity<EFGame>()
            .HasOne(g => g.Guest)
            .WithMany(p => p.GamesAsGuest) // Relie à GamesAsGuest
            .HasForeignKey(g => g.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Player - Winner relation (one-to-many)
        modelBuilder.Entity<EFGame>()
            .HasOne(g => g.Winner)
            .WithMany()
            .HasForeignKey(g => g.WinnerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Player - CurrentTurn relation (one-to-many)
        modelBuilder.Entity<EFGame>()
            .HasOne(g => g.CurrentTurn)
            .WithMany()
            .HasForeignKey(g => g.CurrentTurnId)
            .OnDelete(DeleteBehavior.Restrict);

        // Game - Grid Relation
        modelBuilder.Entity<EFGame>()
            .HasOne(g => g.Grid)
            .WithOne()
            .HasForeignKey<EFGame>(g => g.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Grid - Cells relation
        modelBuilder.Entity<EFCell>()
            .HasOne(c => c.Grid)
            .WithMany(g => g.Cells)
            .HasForeignKey(c => c.GridId);

        modelBuilder.Entity<EFCell>()
            .Property(c => c.TokenColor)
            .IsRequired();
    }
}