using System.Linq;
using TechnicalTask.Models;

namespace TechnicalTask.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TtContext context)
        {
            Organization[] organizations;
            Country[] countries;
            Business[] businesses;
            Family[] families;
            Offering[] offerings;
            Department[] departments;

            context.Database.EnsureCreated();

            //Users
            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new User {Name = "Clancy", Surname = "Johnson", Email = "clancy@gmail.com", Address = "LA"},
                    new User {Name = "Jacob", Surname = "Brown", Email = "jb@gmail.com", Address = "Phoenix"},
                    new User {Name = "Diego", Surname = "Silva", Email = "dsilva@gmail.com", Address = "El Paso"}
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            //Organizations
            if (!context.Organizations.Any())
            {
                organizations = new[]
                {
                    new Organization
                    {
                        Name = "Stark Industries",
                        Code = "stind",
                        OrganizationType = OrganizationType.IncorporatedCompany,
                        Owner = "Tony Stark"
                    },
                    new Organization
                    {
                        Name = "Daily Bugle",
                        Code = "dbug",
                        OrganizationType = OrganizationType.LimitedLiabilityCompany,
                        Owner = "J. J. Jameson"
                    },
                };

                context.Organizations.AddRange(organizations);
                context.SaveChanges();
            }
            else
            {
                organizations = context.Organizations.ToArray();
            }

            //Countries
            if (!context.Countries.Any())
            {
                countries = new[]
                {
                    new Country {Name = "United States of America", Code = "US"},
                    new Country {Name = "Germany", Code = "DE"},
                    new Country {Name = "Japan", Code = "JP"}
                };
                
                context.Countries.AddRange(countries);
                context.SaveChanges();
            }
            else
            {
                countries = context.Countries.ToArray();
            }

            //OrganizationCountry
            if (!context.OrganizationCountries.Any())
            {
                var organizationCountries = new[]
                {
                    new OrganizationCountry {OrganizationId = organizations[0].Id, CountryId = countries[0].Id},
                    new OrganizationCountry {OrganizationId = organizations[0].Id, CountryId = countries[1].Id},
                    new OrganizationCountry {OrganizationId = organizations[0].Id, CountryId = countries[2].Id},
                    new OrganizationCountry {OrganizationId = organizations[1].Id, CountryId = countries[0].Id},
                };

                context.OrganizationCountries.AddRange(organizationCountries);
                context.SaveChanges();
            }

            //Businesses
            if (!context.Businesses.Any())
            {
                businesses = new[]
                {
                    new Business {Name = "GIS", CountryId = countries[0].Id},
                    new Business {Name = "CEO", CountryId = countries[2].Id}
                };

                context.Businesses.AddRange(businesses);
                context.SaveChanges();
            }
            else
            {
                businesses = context.Businesses.ToArray();
            }

            //Families
            if (!context.Families.Any())
            {
                families = new[]
                {
                    new Family {Name = "Cloud", BusinessId = businesses[0].Id},
                    new Family {Name = "Insurance", BusinessId = businesses[0].Id},
                    new Family {Name = "Cyber", BusinessId = businesses[1].Id}
                };

                context.Families.AddRange(families);
                context.SaveChanges();
            }
            else
            {
                families = context.Families.ToArray();
            }


            //Offerings
            if (!context.Offerings.Any())
            {
                offerings = new[]
                {
                    new Offering {Name = "Cloud Compute", FamilyId = families[1].Id},
                    new Offering {Name = "Cyber Consulting Services", FamilyId = families[0].Id},
                };

                context.Offerings.AddRange(offerings);
                context.SaveChanges();
            }
            else
            {
                offerings = context.Offerings.ToArray();
            }


            //Departments
            if (!context.Departments.Any())
            {
                departments = new[]
                {
                    new Department {Name = "Dept. 1", OfferingId = offerings[0].Id},
                    new Department {Name = "Dept. 2", OfferingId = offerings[1].Id}
                };

                context.Departments.AddRange(departments);
                context.SaveChanges();
            }
            else
            {
                departments = context.Departments.ToArray();
            }
        }
    }
}
