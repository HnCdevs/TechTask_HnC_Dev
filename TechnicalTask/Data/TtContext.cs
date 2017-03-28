using Microsoft.EntityFrameworkCore;
using TechnicalTask.Models;

namespace TechnicalTask.Data
{

    public class TtContext : DbContext
    {
        public TtContext()
        {
        }

        public virtual void SetModify<T>(T item) where T : class
        {
            Entry(item).State = EntityState.Modified;
        }

        public TtContext(DbContextOptions<TtContext> options) : base(options)
        {
        }


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

        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Offering> Offerings { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationCountry> OrganizationCountries { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
