using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDataContext(DbContextOptions<AppDataContext> options) : DbContext(options)
{
    public DbSet<Location> Locations => Set<Location>();
    
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.Title).IsUnique();
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => new {e.Location, e.Name}).IsUnique();
        });

    }
}