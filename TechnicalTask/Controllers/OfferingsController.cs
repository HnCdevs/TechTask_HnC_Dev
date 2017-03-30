using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Offerings")]
    public class OfferingsController : Controller
    {
        private readonly OfferingRepository _repository;
        private readonly ILogger _logger;

        public OfferingsController(OfferingRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger("Offerings Controller");
        }

        // GET: api/Offerings
        /// <summary>
        /// Get a list of offerings. 
        /// </summary>
        /// <returns>Returns a list of offerings.</returns>
        [HttpGet]
        public IEnumerable<Offering> Get()
        {
            _logger.LogInformation("Offerings.Get called. Without arguments.");
            var offerings = _repository.GetList();

            _logger.LogTrace("Offerings.Get ended. Return all Offerings.");
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
            _logger.LogInformation($"Offerings.Get called. Arguments: Id = {id}.");
            var offering = _repository.GetItem(id);

            _logger.LogTrace($"Offerings.Get ended. Return Offering with Id = {id}.");
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
                _logger.LogError("Offerings.Post. Argument \"offering\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Offerings.Post called. Arguments: offering Name = {offering.Name}.");

            if (_repository.IsValid(offering))
            {
                _repository.Create(offering);
                _logger.LogTrace("Offerings.Post ended. Offering successfully created.");
            }
            else
            {
                _logger.LogError("Offerings.Post. Argument: offering Name is invalid.");
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
                _logger.LogError("Offerings.Put. Argument \"offering\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Offerings.Put called. Arguments: Id = {id}, offering Name = {offering.Name}.");

            if (_repository.IsValid(offering))
            {
                _repository.Update(id, offering);
                _logger.LogTrace("Offerings.Put ended. Offering successfully updated.");
            }
            else
            {
                _logger.LogError("Offerings.Put. Argument: offering Name is invalid.");
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
            _logger.LogInformation($"Offerings.Delete called. Arguments: Id = {id}.");

            _repository.Delete(id);
            _logger.LogTrace("Offerings.Delete ended. Offering successfully deleted.");
        }
    }
}
