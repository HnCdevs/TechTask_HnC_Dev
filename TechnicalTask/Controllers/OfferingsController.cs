using System;
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
        private readonly OfferingRepository _repository;

        public OfferingsController(OfferingRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Offerings
        /// <summary>
        /// Get a list of offerings. 
        /// </summary>
        /// <returns>Returns a list of offerings.</returns>
        [HttpGet]
        public IEnumerable<Offering> Get()
        {
            var offerings = _repository.GetList();
            return offerings;
        }

        // GET: api/Offerings/5
        /// <summary>
        /// Get the offering by id.
        /// </summary>
        /// <param name="id">Offering id.</param>
        /// <returns>Returns a single offering.</returns>
        [HttpGet("{id}")]
        public Offering Get(int id)
        {
            var offering = _repository.GetItem(id);
            return offering;
        }

        // POST: api/Offerings
        /// <summary>
        /// Create a offering and stores it in the database.
        /// </summary>
        /// <param name="offering">Data to create a offering.</param>
        [HttpPost]
        public void Post([FromBody]Offering offering)
        {
            if (offering == null)
            {
                throw new ArgumentNullException();
            }

            if (_repository.IsValid(offering))
            {
                _repository.Create(offering);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        // PUT: api/Offerings/5
        /// <summary>
        /// Update (HTTP PUT) an existing offering with new data.
        /// </summary>
        /// <param name="id">Offering id.</param>
        /// <param name="offering">Data to update the offering.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Offering offering)
        {
            if (offering == null)
            {
                throw new ArgumentNullException();
            }

            if (_repository.IsValid(offering))
            {
                _repository.Update(id, offering);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        // DELETE: api/Offerings/5
        /// <summary>
        /// Delete the offering by id.
        /// </summary>
        /// <param name="id">Offering id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
