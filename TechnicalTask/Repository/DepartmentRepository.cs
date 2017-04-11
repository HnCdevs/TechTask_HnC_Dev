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

         
    }
}
