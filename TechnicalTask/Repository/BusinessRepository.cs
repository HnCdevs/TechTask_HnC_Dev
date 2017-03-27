using System;
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

        public override void Create(Business item)
        {
            var country = Context.Countries.Find(item.CountryId);

            if (country != null)
            {
                var businesses = Context.Businesses.Where(x => x.CountryId == country.Id).ToList();

                if (businesses.Any(x => x.Name == item.Name))
                {
                    throw new Exception("The current business already exists in this country!");
                }

                base.Create(item);
            }
            else
            {
                throw new Exception("The selected country doesn't exists!");
            }
        }
    }
}
