using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.Models;

namespace TechnicalTask.Data
{
    public class TtContext : DbContext
    {
        public TtContext(DbContextOptions<TtContext> options) : base(options)
        {
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Offering> Offerings { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationCountry> OrganizationCountries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>().ToTable("Business");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Family>().ToTable("Family");
            modelBuilder.Entity<Offering>().ToTable("Offering");
            modelBuilder.Entity<Organization>().ToTable("Organization");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<OrganizationCountry>().HasKey(x => new {x.OrganizationId, x.CountryId});
            modelBuilder.Entity<OrganizationCountry>()
                .HasOne(x => x.Organization)
                .WithMany(x => x.OrganizationCountries)
                .HasForeignKey(x => x.CountryId);
            modelBuilder.Entity<OrganizationCountry>()
                .HasOne(x => x.Country)
                .WithMany(x => x.OrganizationCountries)
                .HasForeignKey(x => x.OrganizationId);
        }
    }
}
