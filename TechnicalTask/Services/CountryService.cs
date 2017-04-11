using System.Collections.Generic;
using System.Linq;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class CountryService : Service<Country>
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<OrganizationCountry> _organizationCountryRepository;
        private readonly IRepository<Organization> _organizationRepository;

        public CountryService(IRepository<Country> countryRepository, IRepository<OrganizationCountry> organizationCountryRepository, IRepository<Organization> organizationRepository) : base(countryRepository)
        {
            _countryRepository = countryRepository;
            _organizationCountryRepository = organizationCountryRepository;
            _organizationRepository = organizationRepository;
        }       

        public override void Create(Country item)
        {
            var organizationId = item.OrganizationCountries.ToArray()[0].OrganizationId;
            item.OrganizationCountries = new List<OrganizationCountry>{ new OrganizationCountry
            {
                OrganizationId = organizationId,
                CountryId = item.Id
            }};

            _countryRepository.Create(item);
        }        

        public override bool IsValid(Country item)
        {
            var organizationId = item.OrganizationCountries.ToArray()[0].OrganizationId;
            var organization = _organizationRepository.GetItem(organizationId);

            if (organization == null) return false;

            var countries = _organizationCountryRepository.GetList().Where(x => x.OrganizationId == organizationId).ToList();

            return countries.All(x => x.Country.Name != item.Name);
        }
    }
}
