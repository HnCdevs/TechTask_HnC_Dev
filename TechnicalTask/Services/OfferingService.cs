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

        //public override IEnumerable<Offering> GetList()
        //{
        //    var offerings = _offeringRepository.GetList();
        //    var newOfferings = offerings.Select(offering => new Offering
        //        {
        //            Id = offering.Id,
        //            Name = offering.Name,
        //            FamilyId = offering.FamilyId
        //        })
        //        .ToList();

        //    return newOfferings;
        //}

        public override bool IsValid(Offering item)
        {
            var family = _familyRepository.GetItem(item.FamilyId);

            if (family == null) return false;

            var offerings = _offeringRepository.GetList().Where(x => x.FamilyId == family.Id).ToList();

            return offerings.All(x => x.Name != item.Name);
        }
    }
}
