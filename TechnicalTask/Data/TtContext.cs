using Microsoft.EntityFrameworkCore;
using TechnicalTask.Models;

namespace TechnicalTask.Data
{
    public class TtContext : DbContext
    {
        public TtContext()
        {            
        }

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
            modelBuilder.Entity<OrganizationCountry>()
                .HasOne(x => x.Organization)
                .WithMany(x => x.OrganizationCountries)
                .HasForeignKey(x => x.OrganizationId);
            modelBuilder.Entity<OrganizationCountry>()
                .HasOne(x => x.Country)
                .WithMany(x => x.OrganizationCountries)
                .HasForeignKey(x => x.CountryId);
        }
    }
}
