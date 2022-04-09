using System.Security.Principal;
using ApartmentRental.Core.Entities;

namespace ApartmentRental.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

public class MainContext : DbContext
{
    public DbSet<Apartment> Apartment { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Image> Image { get; set; }
    public DbSet<Landlord> Landlord { get; set; }
    public DbSet<Tenant> Tenant { get; set; }
    public DbSet<Address> Address { get; set; }

    public MainContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=dbo.ApartmentRental.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>()
            .HasMany(x => x.Images)
            .WithOne(x => x.Apartment)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Landlord>()
            .HasMany(x => x.Apartments)
            .WithOne(x => x.Landlord)
            .OnDelete(DeleteBehavior.Cascade);
    }
}