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

        public override bool IsValid(Country item)
        {
            var organizationId = GetOrganizationId(item);
            var organization = Context.Organizations.Find(organizationId);

            if (organization == null) return false;

            var countries = Context.OrganizationCountries.Where(x => x.OrganizationId == organizationId).ToList();

            return countries.All(x => x.Country.Name == item.Name);
        }

        public override void Create(Country item)
        {
            base.Create(item);

            var organizationId = GetOrganizationId(item);

            Context.OrganizationCountries.Add(new OrganizationCountry
            {
                OrganizationId = organizationId,
                CountryId = item.Id
            });
            Context.SaveChanges();           
        }

        private static int GetOrganizationId(Country item)
        {
            return item.OrganizationCountries.ToArray()[0].OrganizationId;
        }

        //Consultate
        private void ValidationLogic(CountryInput item)
        {


            //var updatedCountry = item.ConvertToCountry(item);
            //var org = Context.OrganizationCountries.Find(item.OrganizationId);
            //org.OrganizationId = item.OrganizationId;
            //var oldOrgCountry = Context.OrganizationCountries.Find(org.Id);
            //Context.Entry(oldOrgCountry).CurrentValues.SetValues(org);

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
