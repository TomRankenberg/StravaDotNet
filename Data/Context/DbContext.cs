using System.Xml;
using Data.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
    {
        ModelBuilder modelBuilder = new ModelBuilder();
        modelBuilder.Entity<StravaUser>().HasKey(x => x.UserId);
    }

    public DbSet<StravaUser> Users { get; set; }
}
