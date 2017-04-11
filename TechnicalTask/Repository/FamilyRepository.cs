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

        
    }
}
