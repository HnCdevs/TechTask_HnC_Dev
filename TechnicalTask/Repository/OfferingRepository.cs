using System.Linq;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class OfferingRepository : Repository<Offering>
    {
        public OfferingRepository(TtContext context) : base(context)
        {
        }

        public override bool IsValid(Offering item)
        {
            var family = Context.Families.Find(item.FamilyId);

            if (family == null) return false;

            var offerings = Context.Offerings.Where(x => x.FamilyId == family.Id).ToList();

            return offerings.All(x => x.Name != item.Name);
        }       
    }
}
