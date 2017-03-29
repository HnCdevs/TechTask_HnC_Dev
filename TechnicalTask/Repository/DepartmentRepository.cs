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
    }
}
