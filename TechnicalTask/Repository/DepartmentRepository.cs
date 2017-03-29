using System;
using System.Linq;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class DepartmentRepository : Repository<Department>
    {
        public DepartmentRepository(TtContext context) : base(context)
        {
        }

        public override bool IsValid(Department item)
        {
            var offering = Context.Offerings.Find(item.OfferingId);

            if (offering == null) return false;
            var departments = Context.Departments.Where(x => x.OfferingId == offering.Id).ToList();

            return departments.All(x => x.Name != item.Name);
        }

        private void ValidationLogic(Department item)
        {
            var offering = Context.Offerings.Find(item.OfferingId);

            if (offering != null)
            {
                var departments = Context.Departments.Where(x => x.OfferingId == offering.Id).ToList();

                if (departments.Any(x => x.Name == item.Name))
                {
                    throw new Exception("The current department already exists in this offering!");
                }
            }
            else
            {
                throw new Exception("The selected offering doesn't exists!");
            }
        }
    }
}
