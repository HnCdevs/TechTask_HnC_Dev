using System;
using System.Linq;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class CountryRepository : Repository<Country>
    {
        public CountryRepository(TtContext context) : base(context)
        {
        }

        public void Create(CountryInput country)
        {
            var organization = Context.Organizations.Find(country.OrganizationId);

            if (organization != null)
            {
                var countries = Context.OrganizationCountries.Where(x => x.OrganizationId == country.OrganizationId).ToList();

                if (countries.Any(x => x.Country.Name == country.Name))
                {
                    throw new Exception("The current country already exists in this organization!");
                }

                var newCountry = country.ConvertToCountry(country);
                Create(newCountry);
                Context.OrganizationCountries.Add(new OrganizationCountry
                {
                    OrganizationId = country.OrganizationId,
                    CountryId = newCountry.Id
                });
                Context.SaveChanges();
            }
            else
            {
                throw new Exception("The selected organization doesn't exists!");
            }
        }

        //public  void Update(int id, CountryInput item)
        //{
        //    var country = GetItem(id);
        //    var s = country.OrganizationCountries.Any(x => x.)
        //    var organization = Context.Organizations.Find(country.OrganizationId);

        //    if (organization != null)
        //    {
        //        var countries = Context.OrganizationCountries.Where(x => x.OrganizationId == country.OrganizationId).ToList();

        //        if (countries.Any(item => item.Country.Name == country.Name))
        //        {
        //            throw new Exception("The current country already exists in this organization!");
        //        }

        //        var newCountry = country.ConvertToCountry(country);
        //        Create(newCountry);
        //        Context.OrganizationCountries.Add(new OrganizationCountry
        //        {
        //            OrganizationId = country.OrganizationId,
        //            CountryId = newCountry.Id
        //        });
        //        Context.SaveChanges();
        //    }
        //    else
        //    {
        //        throw new Exception("The selected organization doesn't exists!");
        //    }
        //}
    }
}
