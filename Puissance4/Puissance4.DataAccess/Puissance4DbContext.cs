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
            entity.HasOne(c => c.Token).WithMany().HasForeignKey(c => c.TokenId).OnDelete(DeleteBehavior.SetNull);
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