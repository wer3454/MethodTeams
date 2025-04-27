using MethodologyMain.Logic.Entities;
using MethodologyMain.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace MethodTeams.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<UserMainEntity> Users { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<HackathonEntity> Hackathons { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HackathonTagEntity>()
                .HasKey(e => new {e.TagId, e.HackathonId });

            modelBuilder.Entity<UserTagEntity>()
                .HasKey(e => new {e.TagId, e.UserId});

            modelBuilder.Entity<TeamTagEntity>()
                .HasKey(e => new { e.TagId, e.TeamId });

            modelBuilder.Entity<UserTeamEntity>()
                .HasKey(e => new {e.UserId, e.TeamId});

            // Настройка связей между таблицами
            modelBuilder.Entity<HackathonTagEntity>()
                .HasOne<TagEntity>(e => e.Tag)
                .WithMany(e => e.Hacksthons)
                .HasForeignKey(e => e.TagId);

            modelBuilder.Entity<HackathonTagEntity>()
                .HasOne<HackathonEntity>(e => e.Hackathon)
                .WithMany(e => e.Tags)
                .HasForeignKey(e => e.HackathonId);

            modelBuilder.Entity<UserTagEntity>()
                .HasOne<TagEntity>(e => e.Tag)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.TagId);

            modelBuilder.Entity<UserTagEntity>()
                .HasOne<UserMainEntity>(e => e.User)
                .WithMany(e => e.Tags)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<TeamTagEntity>()
                .HasOne<TagEntity>(e => e.Tag)
                .WithMany(e => e.Teams)
                .HasForeignKey(e => e.TagId);

            modelBuilder.Entity<TeamTagEntity>()
                .HasOne<TeamEntity>(e => e.Team)
                .WithMany(e => e.Tags)
                .HasForeignKey(e => e.TeamId);

            modelBuilder.Entity<UserTeamEntity>()
                .HasOne<UserMainEntity>(e => e.User)
                .WithMany(e => e.Teams)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserTeamEntity>()
                .HasOne<TeamEntity>(e => e.Team)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.TeamId);
            //modelBuilder.Entity<TeamEntity>()
            //    .HasMany(t => t.Members)
            //    .WithMany(m => m.Teams)
            //    .UsingEntity<UserTeamEntity>();

            //modelBuilder.Entity<UserMainEntity>()
            //    .HasMany(e => e.Tags)
            //    .WithMany(e => e.Users)
            //    .UsingEntity<UserTagEntity>();

            //modelBuilder.Entity<HackathonEntity>()
            //    .HasMany(e => e.Tags)
            //    .WithMany(e => e.Hacksthons)
            //    .UsingEntity<HackathonTagEntity>();

            modelBuilder.Entity<TeamEntity>()
                .HasOne<HackathonEntity>(e => e.Hackathon)
                .WithMany(e => e.Teams)
                .HasForeignKey(e => e.HackathonId);

            modelBuilder.Entity<HackathonEntity>()
                .HasOne<OrganizationEntity>(e => e.Organization)
                .WithMany(e => e.Hackathons)
                .HasForeignKey(e => e.OrganizationId);
        }
    }
}
