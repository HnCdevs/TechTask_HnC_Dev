using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Offerings")]
    public class OfferingsController : Controller
    {
        private readonly IRepository<Offering> _repository;

        public OfferingsController(IRepository<Offering> repository)
        {
            _repository = repository;
        }

        // GET: api/Offerings
        [HttpGet]
        public IEnumerable<Offering> Get()
        {
            var offerings = _repository.GetList();
            return offerings;
        }

        // GET: api/Offerings/5
        [HttpGet("{id}")]
        public Offering Get(int id)
        {
            var offering = _repository.GetItem(id);
            return offering;
        }
        
        // POST: api/Offerings
        [HttpPost]
        public void Post([FromBody]Offering offering)
        {
            _repository.Create(offering);
        }
        
        // PUT: api/Offerings
        [HttpPut]
        public void Put(int id, [FromBody]Offering offering)
        {
            _repository.Update(id, offering);
        }

        // DELETE: api/Offerings/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
