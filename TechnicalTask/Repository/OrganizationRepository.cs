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

    }
}
