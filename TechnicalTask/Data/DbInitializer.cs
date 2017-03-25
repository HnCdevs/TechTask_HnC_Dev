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
            //var organizationCountries = new[]
            //{
            var o1 = new OrganizationCountry {OrganizationId = organizations[0].Id, CountryId = countries[0].Id};
            var o2 = new OrganizationCountry {OrganizationId = organizations[0].Id, CountryId = countries[1].Id};
            var o3 = new OrganizationCountry {OrganizationId = organizations[0].Id, CountryId = countries[2].Id};
            var o4 = new OrganizationCountry {OrganizationId = organizations[1].Id, CountryId = countries[0].Id};
            //};

            context.OrganizationCountries.AddRange(new [] {o1, o2, o4, o3});
            context.SaveChanges();

            //Businesses
            var businesses = new[]
            {
                new Business {Name = "GIS", CountryId = 1},
                new Business {Name = "CEO", CountryId = 3}
            };

            foreach (var business in businesses)
            {
                context.Businesses.Add(business);
            }
            context.SaveChanges();

            //Families
            var families = new[]
            {
                new Family {Name = "Cloud", BusinessId = 1},
                new Family {Name = "Insurance", BusinessId = 1},
                new Family {Name = "Cyber", BusinessId = 2}
            };

            foreach (var family in families)
            {
                context.Families.Add(family);
            }
            context.SaveChanges();

            //Offerings
            var offerings = new[]
            {
                new Offering {Name = "Cloud Compute", FamilyId = 2},
                new Offering {Name = "Cyber Consulting Services", FamilyId = 1},
            };

            foreach (var offering in offerings)
            {
                context.Offerings.Add(offering);
            }
            context.SaveChanges();

            //Departments
            var departments = new[]
            {
                new Department {Name = "Dept. 1", OfferingId = 1},
                new Department {Name = "Dept. 2", OfferingId = 2}
            };

            foreach (var department in departments)
            {
                context.Departments.Add(department);
            }
            context.SaveChanges();
        }
    }
}
