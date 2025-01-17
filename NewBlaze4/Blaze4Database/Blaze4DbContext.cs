using Microsoft.EntityFrameworkCore;
using Blaze4Shared.Models;

namespace Blaze4Database
{
    public class Blaze4DbContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;

        public Blaze4DbContext(DbContextOptions<Blaze4DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration pour l'entité Player
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Username).IsRequired();
                entity.Property(p => p.PasswordHash).IsRequired();
            });

            // Configuration pour l'entité Game
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.Id);

                // Configuration de la relation avec Host
                entity.HasOne(g => g.Host)
                    .WithMany(p => p.Games)
                    .HasForeignKey(g => g.HostId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuration de la relation avec Guest
                entity.HasOne(g => g.Guest)
                    .WithMany()
                    .HasForeignKey(g => g.GuestId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuration supplémentaire (si nécessaire)
                entity.Property(g => g.Status).IsRequired();
                
                // Configure Grid comme un type possédé
                entity.OwnsOne(g => g.Grid, grid =>
                {
                    
                });
            });
            
            modelBuilder.Entity<Grid>(entity =>
            {
                entity.HasKey(g => g.Id);
            });
        }
    }
}