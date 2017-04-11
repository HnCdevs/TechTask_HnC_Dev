using System.Linq;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class BusinessRepository : Repository<Business>
    {
        public BusinessRepository(TtContext context) : base(context)
        {
        }


    }
}
