using System.Collections.Generic;
using System.Linq;
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

        public override IEnumerable<Business> GetList()
        {
            var businesses = from item
                in _businessRepository.GetList()
                select new Business { Id = item.Id, CountryId = item.CountryId, Name = item.Name };

            return businesses;
        }

        public override Business GetItem(int id)
        {
            var item = _businessRepository.GetItem(id);
            var business = new Business
            {
                Id = item.Id,
                CountryId = item.CountryId,
                Name = item.Name
            };

            return business;
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
