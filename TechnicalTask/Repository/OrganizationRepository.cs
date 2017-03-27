using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class OrganizationRepository : Repository<Organization>
    {
        public OrganizationRepository(TtContext context) : base(context)
        {
        }

        public override IEnumerable<Organization> GetList()
        {
            var organizations = Context.Organizations.Include(x => x.OrganizationCountries).ThenInclude(x => x.Country).ThenInclude(x => x.Businesses).ThenInclude(x => x.Families).ThenInclude(x => x.Offerings).ThenInclude(x => x.Departments);
            return organizations;
        }
    }
}
