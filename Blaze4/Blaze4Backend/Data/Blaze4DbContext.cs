using Blaze4.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Blaze4Backend.Data;

public class Blaze4DbContext : DbContext
{
    public Blaze4DbContext(DbContextOptions<Blaze4DbContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Cell> Cells { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cell>(entity =>
        {
            // Configuration de la clé composite
            entity.HasKey(c => new { c.Row, c.Column });

            // Configuration des propriétés
            entity.Property(c => c.Row).IsRequired();
            entity.Property(c => c.Column).IsRequired();
        
            // Ignorer Token s'il ne doit pas être mappé en base de données
            entity.Ignore(c => c.Token);
        });
    }

}
