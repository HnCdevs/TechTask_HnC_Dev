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

        public void Create(CountryInput item)
        {
            ValidationLogic(item);

            var newCountry = item.ConvertToCountry(item);
            Create(newCountry);

            Context.OrganizationCountries.Add(new OrganizationCountry
            {
                OrganizationId = item.OrganizationId,
                CountryId = newCountry.Id
            });
            Context.SaveChanges();           
        }

        public void Update(int id, CountryInput item)
        {
            ValidationLogic(item);

            //var updatedCountry = item.ConvertToCountry(item);
            //var org = Context.OrganizationCountries.Find(item.OrganizationId);
            //org.OrganizationId = item.OrganizationId;
            //var oldOrgCountry = Context.OrganizationCountries.Find(org.Id);
            //Context.Entry(oldOrgCountry).CurrentValues.SetValues(org);

            var updatedCountry = item.ConvertToCountry(item);
            updatedCountry.Id = id;
            Update(id, updatedCountry);
        }

        //Consultate
        private void ValidationLogic(CountryInput item)
        {
            var organization = Context.Organizations.Find(item.OrganizationId);

            if (organization != null)
            {
                var countries = Context.OrganizationCountries.Where(x => x.OrganizationId == item.OrganizationId).ToList();

                if (countries.Any(x => x.Country.Name == item.Name))
                {
                    throw new Exception("The current country already exists in this organization!");
                }
            }
            else
            {
                throw new Exception("The selected organization doesn't exists!");
            }
        }
    }
}
