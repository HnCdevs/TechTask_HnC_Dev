using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Organizations")]
    public class OrganizationsController : Controller
    {
        private readonly IRepository<Organization> _repository;

        public OrganizationsController(IRepository<Organization> repository)
        {
            _repository = repository;
        }

        // GET: api/Organizations
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            var organizations = _repository.GetList();
            return organizations;
        }

        // GET: api/Organizations/5
        [HttpGet("{id}")]
        public Organization Get(int id)
        {
            var organization = _repository.GetItem(id);
            return organization;
        }
        
        // POST: api/Organizations
        [HttpPost]
        public void Post([FromBody]Organization organization)
        {
            _repository.Create(organization);
        }
        
        // PUT: api/Organizations/5
        [HttpPut("{id}")]
        public void Put([FromBody]Organization organization)
        {
            _repository.Update(organization);
        }
        
        // DELETE: api/Organizations/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
