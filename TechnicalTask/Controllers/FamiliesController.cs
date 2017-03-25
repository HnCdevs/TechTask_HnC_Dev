using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Families")]
    public class FamiliesController : Controller
    {
        private readonly IRepository<Family> _repository;

        public FamiliesController(IRepository<Family> repository)
        {
            _repository = repository;
        }

        // GET: api/Families
        [HttpGet]
        public IEnumerable<Family> Get()
        {
            var families = _repository.GetList();
            return families;
        }

        // GET: api/Families/5
        [HttpGet("{id}")]
        public Family Get(int id)
        {
            var family = _repository.GetItem(id);
            return family;
        }
        
        // POST: api/Families
        [HttpPost]
        public void Post([FromBody]Family family)
        {
            _repository.Create(family);
        }
        
        // PUT: api/Families/5
        [HttpPut("{id}")]
        public void Put([FromBody]Family family)
        {
            _repository.Update(family);
        }

        // DELETE: api/Families/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
