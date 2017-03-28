using System;
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

        public override void Create(Family item)
        {
            ValidationLogic(item);
            base.Create(item);
        }

        public override void Update(int id, Family item)
        {
            ValidationLogic(item);
            base.Update(id, item);
        }

        private void ValidationLogic(Family item)
        {
            var business = Context.Businesses.Find(item.BusinessId);

            if (business != null)
            {
                var families = Context.Families.Where(x => x.BusinessId == item.BusinessId);

                if (families.Any(x => x.Name == item.Name))
                {
                    throw new Exception("The current family already exists in this business!");
                }
            }
            else
            {
                throw new Exception("The selected business doesn't exists!");
            }
        }
    }
}
