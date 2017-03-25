using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class OrganizationRepository : Repository<Organization>
    {
        public OrganizationRepository(TtContext context) : base(context)
        {
        }

        //public override IEnumerable<Organization> GetList()
        //{
        //    var organizations = Context.
        //    return ;
        //}
    }
}
