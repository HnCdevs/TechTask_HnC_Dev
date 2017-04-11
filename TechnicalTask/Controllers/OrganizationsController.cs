using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Services;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Organizations")]
    public class OrganizationsController : Controller
    {
        private readonly IService<Organization> _service;
        private readonly ILogger _logger;

        public OrganizationsController(IService<Organization> service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger("Organizations Controller");
        }

        //Get: api/Organizations/Tree
        /// <summary>
        /// Get the tree.
        /// </summary>
        /// <returns>Returns whole tree.</returns>
        [HttpGet]
        [Route("Tree")]
        public IEnumerable<Organization> GetTree()
        {
            return null;
        }


        // GET: api/Organizations
        /// <summary>
        /// Get a list of organizations.
        /// </summary>
        /// <returns>Returns a list of organizations.</returns>
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            _logger.LogInformation("Organizations.Get called. Without arguments.");
            var organizations = _service.GetList();

            _logger.LogTrace("Organizations.Get ended. Return all Organizations.");
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
            _logger.LogInformation($"Organizations.Get called. Arguments: Id = {id}.");
            var organization = _service.GetItem(id);

            _logger.LogTrace($"Organizations.Get ended. Return Organization with Id = {id}.");
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
                _logger.LogError("Organizations.Post. Argument \"organization\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Organizations.Post called. Arguments: organization Name = {organization.Name}.");

            _service.Create(organization);
            _logger.LogTrace("Organizations.Post ended. Organization successfully created.");
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
                _logger.LogError("Organizations.Put. Argument \"organization\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Organizations.Put called. Arguments: Id = {id}, organization Name = {organization.Name}.");

            _service.Update(id, organization);
            _logger.LogTrace("Organizations.Put ended. Organization successfully updated.");
        }

        // DELETE: api/Organizations/5
        /// <summary>
        /// Delete the organization by id.
        /// </summary>
        /// <param name="id">Organization id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Organizations.Delete called. Arguments: Id = {id}.");

            _service.Delete(id);
            _logger.LogTrace("Organizations.Delete ended. Organization successfully deleted.");
        }
    }
}
