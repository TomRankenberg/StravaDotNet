using System.Xml;
using Data.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
    {
    }

    public DbSet<StravaUser> Users { get; set; }
}
