using Microsoft.EntityFrameworkCore;
using Blaze4Shared.Models;

namespace Blaze4Database
{
    public class Blaze4DbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        public Blaze4DbContext(DbContextOptions<Blaze4DbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Username).IsRequired();
                entity.Property(p => p.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.HasOne(g => g.Host).WithMany(p => p.Games).HasForeignKey(g => g.HostId);
                entity.HasOne(g => g.Guest).WithMany().HasForeignKey(g => g.GuestId);
            });
        }
    }
}