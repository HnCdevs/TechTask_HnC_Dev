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

        public override void Create(Department item)
        {
            ValidationLogic(item);
            base.Create(item);
        }

        public override void Update(int id, Department item)
        {
            ValidationLogic(item);
            base.Update(id, item);
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
