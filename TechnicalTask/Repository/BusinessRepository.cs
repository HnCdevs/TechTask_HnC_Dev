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
            ValidationLogic(item);
            base.Create(item);
        }

        public override void Update(int id, Business item)
        {
            ValidationLogic(item);
            base.Update(id, item);
        }

        public override bool IsValid(Business item)
        {
            throw new NotImplementedException();
        }

        private void ValidationLogic(Business item)
        {
            var country = Context.Countries.Find(item.CountryId);

            if (country != null)
            {
                var businesses = Context.Businesses.Where(x => x.CountryId == country.Id).ToList();

                if (businesses.Any(x => x.Name == item.Name))
                {
                    throw new Exception("The current business already exists in this country!");
                }
            }
            else
            {
                throw new Exception("The selected country doesn't exists!");
            }
        }
    }
}
