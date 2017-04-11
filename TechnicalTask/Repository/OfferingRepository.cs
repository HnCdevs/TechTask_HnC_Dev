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

        
    }
}
