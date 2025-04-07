using MethodTeams.Models;
using Microsoft.EntityFrameworkCore;

namespace MethodTeams.Data
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options) { }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей между таблицами
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(m => m.Team)
                .HasForeignKey(m => m.TeamId);

            // Индексы для оптимизации запросов
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.EventId);

            modelBuilder.Entity<TeamMember>()
                .HasIndex(m => m.UserId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Password");
        }
    }
}
