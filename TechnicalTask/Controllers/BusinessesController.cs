using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Services;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Businesses")]
    [Authorize]
    public class BusinessesController : Controller
    {
        private readonly IService<Business> _service;
        private readonly ILogger _logger;

        public BusinessesController(IService<Business> service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger("Businesses Controller");
        }

        // GET: api/Businesses
        /// <summary>
        /// Get a list of businesses.
        /// </summary>
        /// <returns>Returns a list of businesses.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Business> Get()
        {
            _logger.LogInformation("Businesses.Get called. Without arguments.");
            var businesses = _service.GetList();

            _logger.LogTrace("Businesses.Get ended. Return all Businesses.");
            return businesses;
        }

        // GET: api/Businesses/5
        /// <summary>
        /// Get the business by id.
        /// </summary>
        /// <param name="id">Business id.</param>
        /// <returns>Returns a single business.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public Business Get(int id)
        {
            _logger.LogInformation($"Businesses.Get called. Arguments: Id = {id}.");
            var business = _service.GetItem(id);

            _logger.LogTrace($"Businesses.Get ended. Return Business with Id = {id}.");
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
            if (business == null)
            {
                _logger.LogError("Businesses.Post. Argument \"business\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Businesses.Post called. Arguments: business Name = {business.Name}.");

            if (_service.IsValid(business))
            {
                _service.Create(business);
                _logger.LogTrace("Businesses.Post ended. Business successfully created.");
            }
            else
            {
                _logger.LogError("Businesses.Post. Argument: business Name is invalid.");
                throw new ArgumentException();
            }
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
            if (business == null)
            {
                _logger.LogError("Businesses.Put. Argument \"business\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Businesses.Put called. Arguments: Id = {id}, business Name = {business.Name}.");

            if (_service.IsValid(business))
            {
                _service.Update(id, business);
                _logger.LogTrace("Businesses.Put ended. Business successfully updated.");
            }
            else
            {
                _logger.LogError("Businesses.Put. Argument: business Name is invalid.");
                throw new ArgumentException();
            }
        }

        // DELETE: api/Businesses/5
        /// <summary>
        /// Delete the business by id.
        /// </summary>
        /// <param name="id">Business id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Businesses.Delete called. Arguments: Id = {id}.");

            _service.Delete(id);
            _logger.LogTrace("Businesses.Delete ended. Business successfully deleted.");
        }
    }
}
