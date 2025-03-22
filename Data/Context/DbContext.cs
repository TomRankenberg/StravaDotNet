using Data.Models;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StravaUser> Users { get; set; }
        public virtual DbSet<DetailedActivity> Activities { get; set; }
        public virtual DbSet<DetailedSegmentEffort> SegmentEfforts { get; set; }
        public virtual DbSet<PolylineMap> PolylineMaps { get; set; }
        public virtual DbSet<MetaAthlete> MetaAthletes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StravaUser>().HasKey(x => x.UserId);

            modelBuilder.Entity<DetailedActivity>().HasKey(x => x.Id);
            modelBuilder.Entity<DetailedActivity>()
                .HasMany(x => x.SegmentEfforts)
                .WithOne(x => x.DetailedActivity)
                .HasForeignKey(x => x.ActivityId);

            modelBuilder.Entity<MetaAthlete>()
                .HasMany(x => x.Activities)
                .WithOne(x => x.Athlete)
                .HasForeignKey(x => x.AthleteId);

            modelBuilder.Entity<DetailedActivity>()
                .HasOne(x => x.Map)
                .WithOne(x => x.Activity)
                .HasForeignKey<DetailedActivity>(x => x.MapId)
                .IsRequired(false);

            modelBuilder.Entity<MetaActivity>().HasKey(x => x.Id);
            modelBuilder.Entity<MetaAthlete>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
