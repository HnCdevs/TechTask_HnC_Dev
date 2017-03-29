using System;
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
        private readonly OrganizationRepository _repository;

        public OrganizationsController(OrganizationRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Organizations
        /// <summary>
        /// Get a list of organizations.
        /// </summary>
        /// <returns>Returns a list of organizations.</returns>
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            var organizations = _repository.GetList();
            return organizations;
        }

        // GET: api/Organizations/5
        /// <summary>
        /// Get the organization by id.
        /// </summary>
        /// <param name="id">Organization id.</param>
        /// <returns>Returns a single organization.</returns>
        [HttpGet("{id}")]
        public Organization Get(int id)
        {
            var organization = _repository.GetItem(id);
            return organization;
        }

        // POST: api/Organizations
        /// <summary>
        /// Create a organization and stores it in the database.
        /// </summary>
        /// <param name="organization">Data to create a organization.</param>
        [HttpPost]
        public void Post([FromBody]Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException();
            }

            if (_repository.IsValid(organization))
            {
                _repository.Create(organization);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        // PUT: api/Organizations/5
        /// <summary>
        /// Update (HTTP PUT) an existing organization with new data.
        /// </summary>
        /// <param name="id">Organization id.</param>
        /// <param name="organization">Data to update the organization.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Organization organization)
        {
            if (organization == null)
            {
                throw new ArgumentNullException();
            }

            if (_repository.IsValid(organization))
            {
                _repository.Update(id, organization);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        // DELETE: api/Organizations/5
        /// <summary>
        /// Delete the organization by id.
        /// </summary>
        /// <param name="id">Organization id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
