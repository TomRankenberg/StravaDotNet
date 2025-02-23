using System.Security.Policy;
using Data.Models;
using Data.Models.Strava;
using Microsoft.EntityFrameworkCore;
using Strava.NET.Model;

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
        //public virtual DbSet<MetaAthlete> Athletes { get; set; }
        //public virtual DbSet<SummaryGear> SummaryGears { get; set; }
        public virtual DbSet<PolylineMap> PolylineMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StravaUser>().HasKey(x => x.UserId);
            
            modelBuilder.Entity<DetailedActivity>().HasKey(x => x.Id);
            modelBuilder.Entity<DetailedActivity>().HasMany(x => x.SegmentEfforts).
                WithOne(x => x.DetailedActivity).HasForeignKey(x => x.Id);


            //modelBuilder.Entity<MetaAthlete>().
            //    HasMany(x => x.Activities).
            //    WithOne(x => x.Athlete).
            //    HasForeignKey(x => x.AthleteId);

            //modelBuilder.Entity<SummaryGear>().
            //      HasMany(x => x.Activities).
            //      WithOne(x => x.Gear).
            //      HasForeignKey(x => x.GearId).
            //      IsRequired(false);

            //modelBuilder.Entity<DetailedActivity>().
            //    HasOne(x => x.Map).
            //    WithOne(x => x.Activity).
            //    HasForeignKey<DetailedActivity>(x => x.MapId).
            //    IsRequired(false);

            //modelBuilder.Entity<MetaAthlete>().HasKey(x => x.Id);
            //modelBuilder.Entity<SummaryGear>().HasKey(x => x.Id);
            //modelBuilder.Entity<PolylineMap>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}