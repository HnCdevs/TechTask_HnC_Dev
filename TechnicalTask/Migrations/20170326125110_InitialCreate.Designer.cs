using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Migrations
{
    [DbContext(typeof(TtContext))]
    [Migration("20170326125110_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechnicalTask.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("TechnicalTask.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("TechnicalTask.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("OfferingId");

                    b.HasKey("Id");

                    b.HasIndex("OfferingId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("TechnicalTask.Models.Family", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Family");
                });

            modelBuilder.Entity("TechnicalTask.Models.Offering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FamilyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.ToTable("Offering");
                });

            modelBuilder.Entity("TechnicalTask.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.Property<int>("OrganizationType");

                    b.Property<string>("Owner");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("TechnicalTask.Models.OrganizationCountry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationCountries");
                });

            modelBuilder.Entity("TechnicalTask.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TechnicalTask.Models.Business", b =>
                {
                    b.HasOne("TechnicalTask.Models.Country")
                        .WithMany("Businesses")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TechnicalTask.Models.Department", b =>
                {
                    b.HasOne("TechnicalTask.Models.Offering", "Offering")
                        .WithMany("Departments")
                        .HasForeignKey("OfferingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TechnicalTask.Models.Family", b =>
                {
                    b.HasOne("TechnicalTask.Models.Business")
                        .WithMany("Families")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TechnicalTask.Models.Offering", b =>
                {
                    b.HasOne("TechnicalTask.Models.Family")
                        .WithMany("Offerings")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TechnicalTask.Models.OrganizationCountry", b =>
                {
                    b.HasOne("TechnicalTask.Models.Country", "Country")
                        .WithMany("OrganizationCountries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TechnicalTask.Models.Organization", "Organization")
                        .WithMany("OrganizationCountries")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
