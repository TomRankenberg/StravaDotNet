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
        public virtual DbSet<SummarySegment> Segments { get; set; }
        public virtual DbSet<StreamSet> Streams { get; set; }
        public virtual DbSet<TimeStream> TimeStreams { get; set; }
        public virtual DbSet<LatLngStream> LatLngStreams { get; set; }
        public virtual DbSet<AltitudeStream> AltitudeStreams { get; set; }
        public virtual DbSet<DistanceStream> DistanceStreams { get; set; }
        public virtual DbSet<SmoothVelocityStream> SmoothVelocityStreams { get; set; }
        public virtual DbSet<HeartrateStream> HeartrateStreams { get; set; }
        public virtual DbSet<CadenceStream> CadenceStreams { get; set; }
        public virtual DbSet<SmoothGradeStream> SmoothGradeStreams { get; set; }
        public virtual DbSet<MovingStream> MovingStreams { get; set; }
        public virtual DbSet<TemperatureStream> TemperatureStreams { get; set; }
        public virtual DbSet<PowerStream> PowerStreams { get; set; }
        


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

            modelBuilder.Entity<DetailedSegmentEffort>()
                .HasOne(dse => dse.Segment)
                .WithMany()
                .HasForeignKey(dse => dse.SegmentId)
                .IsRequired(false);

            modelBuilder.Entity<StreamSet>().HasKey(s => s.StreamSetId);
            modelBuilder.Entity<StreamSet>()
                .HasOne<DetailedActivity>()
                .WithOne()
                .HasForeignKey<StreamSet>(ss => ss.ActivityId);

            modelBuilder.Entity<TimeStream>().HasKey(ts => ts.TimeStreamId);
            modelBuilder.Entity<TimeStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<TimeStream>(ts => ts.StreamSetId);

            modelBuilder.Entity<DistanceStream>().HasKey(ts => ts.DistanceStreamId);
            modelBuilder.Entity<DistanceStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<DistanceStream>(ds => ds.StreamSetId);

            modelBuilder.Entity<LatLngStream>().HasKey(ts => ts.LatLngStreamId);
            modelBuilder.Entity<LatLngStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<LatLngStream>(ls => ls.StreamSetId);

            modelBuilder.Entity<AltitudeStream>().HasKey(ts => ts.AltitudeStreamId);
            modelBuilder.Entity<AltitudeStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<AltitudeStream>(als => als.StreamSetId);

            modelBuilder.Entity<SmoothVelocityStream>().HasKey(ts => ts.SmoothVelocityStreamId);
            modelBuilder.Entity<SmoothVelocityStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<SmoothVelocityStream>(svs => svs.StreamSetId);

            modelBuilder.Entity<HeartrateStream>().HasKey(ts => ts.HeartrateStreamId);
            modelBuilder.Entity<HeartrateStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<HeartrateStream>(hs => hs.StreamSetId);

            modelBuilder.Entity<CadenceStream>().HasKey(ts => ts.CadenceStreamId);
            modelBuilder.Entity<CadenceStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<CadenceStream>(cs => cs.StreamSetId);

            modelBuilder.Entity<PowerStream>().HasKey(ts => ts.PowerStreamId);
            modelBuilder.Entity<PowerStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<PowerStream>(ps => ps.StreamSetId);

            modelBuilder.Entity<TemperatureStream>().HasKey(ts => ts.TemperatureStreamId);
            modelBuilder.Entity<TemperatureStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<TemperatureStream>(ts => ts.StreamSetId);

            modelBuilder.Entity<MovingStream>().HasKey(ts => ts.MovingStreamId);
            modelBuilder.Entity<MovingStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<MovingStream>(ms => ms.StreamSetId);

            modelBuilder.Entity<SmoothGradeStream>().HasKey(ts => ts.SmoothGradeId);
            modelBuilder.Entity<SmoothGradeStream>()
                .HasOne<StreamSet>()
                .WithOne()
                .HasForeignKey<SmoothGradeStream>(sgs => sgs.StreamSetId);

            modelBuilder.Entity<MetaActivity>().HasKey(x => x.Id);
            modelBuilder.Entity<MetaAthlete>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
