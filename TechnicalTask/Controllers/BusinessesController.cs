using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Businesses")]
    public class BusinessesController : Controller
    {
        private readonly IRepository<Business> _repository;

        public BusinessesController(IRepository<Business> repository)
        {
            _repository = repository;
        }

        // GET: api/Businesses
        [HttpGet]
        public IEnumerable<Business> Get()
        {
            var businesses = _repository.GetList();
            return businesses;
        }

        // GET: api/Businesses/5
        [HttpGet("{id}")]
        public Business Get(int id)
        {
            var business = _repository.GetItem(id);
            return business;
        }
        
        // POST: api/Businesses
        [HttpPost]
        public void Post([FromBody]Business business)
        {
            _repository.Create(business);
        }
        
        // PUT: api/Businesses/5
        [HttpPut("{id}")]
        public void Put([FromBody]Business business)
        {
            _repository.Update(business);
        }

        // DELETE: api/Businesses/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
