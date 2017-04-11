using System.Linq;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class CountryRepository : Repository<Country>
    {
        public CountryRepository(TtContext context) : base(context)
        {
        }

       
    }
}
