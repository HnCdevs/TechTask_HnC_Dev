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

        public override bool IsValid(Business item)
        {
            var country = Context.Countries.Find(item.CountryId);

            if (country == null) return false;

            var businesses = Context.Businesses.Where(x => x.CountryId == country.Id).ToList();

            return businesses.All(x => x.Name != item.Name);
        }     
    }
}
