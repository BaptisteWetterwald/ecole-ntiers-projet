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
        // Configuration de PlayerEntity
        modelBuilder.Entity<PlayerEntity>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Login).IsRequired().HasMaxLength(100);
            entity.Property(p => p.PasswordHash).IsRequired();
        });

        // Configuration de GameEntity
        modelBuilder.Entity<GameEntity>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Status).IsRequired().HasMaxLength(20);

            // Relation avec Grid
            entity.HasOne(g => g.Grid)
                .WithOne()
                .HasForeignKey<GameEntity>(g => g.GridId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relation avec Host
            entity.HasOne(g => g.Host)
                .WithMany()
                .HasForeignKey(g => g.HostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec Guest
            entity.HasOne(g => g.Guest)
                .WithMany()
                .HasForeignKey(g => g.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec CurrentTurn
            entity.HasOne(g => g.CurrentTurn)
                .WithMany()
                .HasForeignKey(g => g.CurrentTurnId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec Winner
            entity.HasOne(g => g.Winner)
                .WithMany()
                .HasForeignKey(g => g.WinnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuration de GridEntity
        modelBuilder.Entity<GridEntity>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Rows).IsRequired();
            entity.Property(g => g.Columns).IsRequired();

            // Relation avec Cell
            entity.HasMany(g => g.Cells)
                .WithOne()
                .HasForeignKey(c => c.GridId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuration de CellEntity
        modelBuilder.Entity<CellEntity>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Row).IsRequired();
            entity.Property(c => c.Column).IsRequired();

            // Relation avec Token
            entity.HasOne(c => c.Token)
                .WithOne()
                .HasForeignKey<TokenEntity>(t => t.CellId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuration de TokenEntity
        modelBuilder.Entity<TokenEntity>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Color).IsRequired().HasMaxLength(10);
        });
    }

    
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Doc: https://learn.microsoft.com/fr-fr/ef/core/modeling/relationships/one-to-many#one-to-many-without-navigation-to-principal-and-with-shadow-foreign-key
        
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
            // Jsp s'il faut rajouter le GridId (qui serait une foreign key ou si ça suffit si on le gère dans la config de Grid) 
            
            // Relation avec Token
            entity.HasOne(c => c.Token)
                .WithOne()
                .HasForeignKey<TokenEntity>(t => t.CellId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Configuration pour Grid
        modelBuilder.Entity<GridEntity>(entity =>
        {
            entity.HasKey(g => g.Id);
            
            // Idem, même question pour le GameId

            entity.Property(g => g.Rows).IsRequired();
            entity.Property(g => g.Columns).IsRequired();

            // Relation avec Cell
            entity.HasMany(g => g.Cells)
                .WithOne()
                .HasForeignKey(c =>c.GridId)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade des Cells si une Grid est supprimée
        });
        
        // Configuration pour Game
        modelBuilder.Entity<GameEntity>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Status).HasMaxLength(20).IsRequired();

            // Relation avec Grid
            entity.HasOne(g => g.Grid)
                .WithOne()
                .HasForeignKey<GridEntity>(g => g.GameId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Relation avec Host
            entity.HasOne(g => g.Host)
                .WithMany(p => p.Games) // gros doute sur ce one to many
                .HasForeignKey(g => g.HostId)
                .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression cascade de l'hôte

            entity.HasOne(g => g.Guest)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(g => g.CurrentTurn)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.Id)
                .OnDelete(DeleteBehavior.Restrict); // Peut-être tester cascade si ça reste
            
            entity.HasOne(g => g.Winner)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.Id)
                .OnDelete(DeleteBehavior.Restrict); // Peut-être tester cascade si ça reste
        });
        
        // Configuration pour Player
        modelBuilder.Entity<PlayerEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Login).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();

            // Là, c'est la merde, je pense
            entity.HasMany(p => p.Games)
                .WithMany();
        });

    }*/
    
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