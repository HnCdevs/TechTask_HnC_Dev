using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class OrganizationService : Service<Organization>
    {
        private readonly IRepository<Organization> _organizationRepository;

        public OrganizationService(IRepository<Organization> organizationRepository) : base(organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public virtual IEnumerable<Organization> GetTree()
        {
            return null;
        }

        public override IEnumerable<Organization> GetList()
        {
            var organizations = _organizationRepository.GetList().AsQueryable().Include(x => x.OrganizationCountries).ThenInclude(x => x.Country).ThenInclude(x => x.Businesses).ThenInclude(x => x.Families).ThenInclude(x => x.Offerings).ThenInclude(x => x.Departments);
            return organizations;
        }
    }
}
