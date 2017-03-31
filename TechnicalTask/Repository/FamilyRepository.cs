using System.Linq;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class FamilyRepository : Repository<Family>
    {
        public FamilyRepository(TtContext context) : base(context)
        {
        }

        public override bool IsValid(Family item)
        {
            var business = Context.Businesses.Find(item.BusinessId);

            if (business == null) return false;

            var families = Context.Families.Where(x => x.BusinessId == business.Id).ToList();

            return families.All(x => x.Name != item.Name);
        }
    }
}
