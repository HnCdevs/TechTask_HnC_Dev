using System.Collections.Generic;
using System.Linq;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class OfferingService : Service<Offering>
    {
        private readonly IRepository<Offering> _offeringRepository;
        private readonly IRepository<Family> _familyRepository;

        public OfferingService(IRepository<Offering> offeringRepository, IRepository<Family> familyRepository) : base(offeringRepository)
        {
            _offeringRepository = offeringRepository;
            _familyRepository = familyRepository;
        }

        public override IEnumerable<Offering> GetList()
        {
            var offerings = from item 
                     in _offeringRepository.GetList()
                     select new Offering { Id = item.Id, FamilyId = item.FamilyId, Name = item.Name };

            return offerings;
        }

        public override Offering GetItem(int id)
        {
            var item = _offeringRepository.GetItem(id);
            var offering = new Offering
            {
                Id = item.Id,
                FamilyId = item.FamilyId,
                Name = item.Name
            };

            return offering;
        }

        public override bool IsValid(Offering item)
        {
            var family = _familyRepository.GetItem(item.FamilyId);

            if (family == null) return false;

            var offerings = _offeringRepository.GetList().Where(x => x.FamilyId == family.Id).ToList();

            return offerings.All(x => x.Name != item.Name);
        }
    }
}
