using Microsoft.EntityFrameworkCore;
using Puissance4.DataAccess.Entities;

namespace Puissance4.DataAccess;


public class Puissance4DbContext : DbContext
{
    public DbSet<PlayerEntity> Players { get; set; }
    public DbSet<GameEntity> Games { get; set; }
    public DbSet<GridEntity> Grids { get; set; }
    public DbSet<CellEntity> Cells { get; set; }
    public DbSet<TokenEntity> Tokens { get; set; }
    
    public Puissance4DbContext(DbContextOptions<Puissance4DbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration pour Token
        modelBuilder.Entity<TokenEntity>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Color).IsRequired().HasMaxLength(10); // "Red" ou "Yellow"
        });
        
        // Configuration pour Cell
        modelBuilder.Entity<CellEntity>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Row).IsRequired();
            entity.Property(c => c.Column).IsRequired();
            
            // Relation avec Token
            entity.HasOne(c => c.Token)
                .WithOne()
                .HasForeignKey<CellEntity>(c => c.TokenId)
                .OnDelete(DeleteBehavior.SetNull); // Si un Token est supprimé, Cell reste sans Token
        });
        
        // Configuration pour Grid
        modelBuilder.Entity<GridEntity>(entity =>
        {
            entity.HasKey(g => g.Id);

            // Relation avec Cell
            entity.HasMany(g => g.Cells)
                .WithOne()
                .HasForeignKey(c =>c.Id)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade des Cells si une Grid est supprimée
        });
        
        // Configuration pour Game
        modelBuilder.Entity<GameEntity>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Status).IsRequired();

            // Relation avec Grid
            entity.HasOne(g => g.Grid)
                .WithOne()
                .HasForeignKey<GameEntity>(g => g.GridId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relation avec Host
            entity.HasOne(g => g.Host)
                .WithOne()
                .HasForeignKey<GameEntity>(g => g.HostId)
                .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression cascade de l'hôte

            entity.HasOne(g => g.Guest)
                .WithOne()
                .HasForeignKey<GameEntity>(g => g.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(g => g.CurrentTurn)
                .WithOne()
                .HasForeignKey<GameEntity>(g => g.CurrentTurn)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(g => g.Winner)
                .WithOne()
                .HasForeignKey<GameEntity>(g => g.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // Configuration pour Player
        modelBuilder.Entity<PlayerEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();

            entity.HasMany(p => p.Games)
                .WithOne()
                .HasForeignKey(g => g.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
    
}

    /*public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }

    public class Game
    {
        public int Id { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
    }*/