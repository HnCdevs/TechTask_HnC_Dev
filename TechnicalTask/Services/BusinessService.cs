using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class BusinessService : Service<Business>
    {
        private readonly IRepository<Business> _businessRepository;
        private readonly IRepository<Country> _countryRepository;

        public BusinessService(IRepository<Business> businessRepository, IRepository<Country> countryRepository) : base(businessRepository)
        {
            _businessRepository = businessRepository;
            _countryRepository = countryRepository;
        }

        public override bool IsValid(Business item)
        {
            var country = _countryRepository.GetItem(item.CountryId);

            if (country == null) return false;

            var businesses = _businessRepository.GetList().Where(x => x.CountryId == country.Id).ToList();

            return businesses.All(x => x.Name != item.Name);
        }
    }
}
