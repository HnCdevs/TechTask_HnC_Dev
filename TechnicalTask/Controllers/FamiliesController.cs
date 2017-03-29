using System;
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
        private readonly FamilyRepository _repository;

        public FamiliesController(FamilyRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Families
        /// <summary>
        /// Get a list of families.
        /// </summary>
        /// <returns>Returns a list of families.</returns>
        [HttpGet]
        public IEnumerable<Family> Get()
        {
            var families = _repository.GetList();
            return families;
        }

        // GET: api/Families/5
        /// <summary>
        /// Get the family by id.
        /// </summary>
        /// <param name="id">Family id.</param>
        /// <returns>Returns a single family.</returns>
        [HttpGet("{id}")]
        public Family Get(int id)
        {
            var family = _repository.GetItem(id);
            return family;
        }

        // POST: api/Families
        /// <summary>
        /// Create a family and stores it in the database.
        /// </summary>
        /// <param name="family">Data to create a family.</param>
        [HttpPost]
        public void Post([FromBody]Family family)
        {
            if (family == null)
            {
                throw new ArgumentNullException();
            }

            if (_repository.IsValid(family))
            {
                _repository.Create(family);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        // PUT: api/Families/5
        /// <summary>
        /// Update (HTTP PUT) an existing family with new data.
        /// </summary>
        /// <param name="id">Family id.</param>
        /// <param name="family">Data to update the family.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Family family)
        {
            if (family == null)
            {
                throw new ArgumentNullException();
            }

            if (_repository.IsValid(family))
            {
                _repository.Update(id, family);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        // DELETE: api/Families/5
        /// <summary>
        /// Delete the family by id.
        /// </summary>
        /// <param name="id">Family id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
