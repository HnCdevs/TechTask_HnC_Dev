using System.Collections.Generic;
using System.Linq;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class FamilyService : Service<Family>
    {
        private readonly IRepository<Family> _familyRepository;
        private readonly IRepository<Business> _businessRepository;

        public FamilyService(IRepository<Family> familyRepository, IRepository<Business> businessRepository) : base(familyRepository)
        {
            _familyRepository = familyRepository;
            _businessRepository = businessRepository;
        }

        public override IEnumerable<Family> GetList()
        {
            var families = from item
                in _familyRepository.GetList()
                select new Family { Id = item.Id, BusinessId = item.BusinessId, Name = item.Name };

            return families;
        }

        public override Family GetItem(int id)
        {
            var item = _familyRepository.GetItem(id);
            var family = new Family
            {
                Id = item.Id,
                BusinessId = item.BusinessId,
                Name = item.Name
            };

            return family;
        }

        public override bool IsValid(Family item)
        {
            var business = _businessRepository.GetItem(item.BusinessId);

            if (business == null) return false;

            var families = _familyRepository.GetList().Where(x => x.BusinessId == business.Id).ToList();

            return families.All(x => x.Name != item.Name);
        }
    }
}
