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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StravaUser>().HasKey(x => x.UserId);
            modelBuilder.Entity<DetailedActivity>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
    }

}