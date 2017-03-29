using System.Collections.Generic;
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
        /// <summary>
        /// Get a list of businesses.
        /// </summary>
        /// <returns>Returns a list of businesses.</returns>
        [HttpGet]
        public IEnumerable<Business> Get()
        {
            var businesses = _repository.GetList();
            return businesses;
        }

        // GET: api/Businesses/5
        /// <summary>
        /// Get the business by id.
        /// </summary>
        /// <param name="id">Business id.</param>
        /// <returns>Returns a single business.</returns>
        [HttpGet("{id}")]
        public Business Get(int id)
        {
            var business = _repository.GetItem(id);
            return business;
        }

        // POST: api/Businesses
        /// <summary>
        /// Create a business and stores it in the database.
        /// </summary>
        /// <param name="business">Data to create a business.</param>
        [HttpPost]
        public void Post([FromBody]Business business)
        {
            _repository.Create(business);
        }

        // PUT: api/Businesses/5
        /// <summary>
        /// Update (HTTP PUT) an existing business with new data.
        /// </summary>
        /// <param name="id">Business id.</param>
        /// <param name="business">Data to update the business.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Business business)
        {
            _repository.Update(id, business);
        }

        // DELETE: api/Businesses/5
        /// <summary>
        /// Delete the business by id.
        /// </summary>
        /// <param name="id">Business id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
