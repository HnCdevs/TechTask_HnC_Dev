using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Countries")]
    public class CountriesController : Controller
    {
        private readonly IRepository<Country> _repository;

        public CountriesController(IRepository<Country> repository)
        {
            _repository = repository;
        }

        // GET: api/Countries
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            var countries = _repository.GetList();
            return countries;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public Country Get(int id)
        {
            var country = _repository.GetItem(id);
            return country;
        }
        
        // POST: api/Countries
        [HttpPost]
        public void Post([FromBody]Country country)
        {
            _repository.Create(country);
        }
        
        // PUT: api/Countries/5
        [HttpPut("{id}")]
        public void Put([FromBody]Country country)
        {
            _repository.Update(country);
        }
        
        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
