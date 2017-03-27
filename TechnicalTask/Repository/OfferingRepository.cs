using System;
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

        public override void Create(Offering item)
        {
            var family = Context.Families.Find(item.FamilyId);

            if (family != null)
            {
                var offerings = Context.Offerings.Where(x => x.FamilyId == family.Id).ToList();

                if (offerings.Any(x => x.Name == item.Name))
                {
                    throw new Exception("The current offering already exists in this family!");
                }

                base.Create(item);
            }
            else
            {
                throw new Exception("The selected family doesn't exists!");
            }
        }
    }
}
