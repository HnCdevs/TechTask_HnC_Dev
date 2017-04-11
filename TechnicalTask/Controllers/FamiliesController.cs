using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Services;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Families")]
    public class FamiliesController : Controller
    {
        private readonly IService<Family> _service;
        private readonly ILogger _logger;

        public FamiliesController(IService<Family> service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger("Families Controller");
        }

        // GET: api/Families
        /// <summary>
        /// Get a list of families.
        /// </summary>
        /// <returns>Returns a list of families.</returns>
        [HttpGet]
        public IEnumerable<Family> Get()
        {
            _logger.LogInformation("Families.Get called. Without arguments.");
            var families = _service.GetList();

            _logger.LogTrace("Families.Get ended. Return all Families.");
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
            _logger.LogInformation($"Families.Get called. Arguments: Id = {id}.");
            var family = _service.GetItem(id);

            _logger.LogTrace($"Families.Get ended. Return Family with Id = {id}.");
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
                _logger.LogError("Families.Post. Argument \"family\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Families.Post called. Arguments: family Name = {family.Name}.");

            if (_service.IsValid(family))
            {
                _service.Create(family);
                _logger.LogTrace("Families.Post ended. Family successfully created.");
            }
            else
            {
                _logger.LogError("Families.Post. Argument: family Name is invalid.");
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
                _logger.LogError("Families.Put. Argument \"family\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Families.Put called. Arguments: Id = {id}, family Name = {family.Name}.");

            if (_service.IsValid(family))
            {
                _service.Update(id, family);
                _logger.LogTrace("Families.Put ended. Family successfully updated.");
            }
            else
            {
                _logger.LogError("Families.Put. Argument: family Name is invalid.");
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
            _logger.LogInformation($"Families.Delete called. Arguments: Id = {id}.");

            _service.Delete(id);
            _logger.LogTrace("Families.Delete ended. Family successfully deleted.");
        }
    }
}
