using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Services;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Countries")]
    public class CountriesController : Controller
    {
        private readonly IService<Country> _service;
        private readonly ILogger _logger;

        public CountriesController(IService<Country> service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger("Countries Controller");
        }

        // GET: api/Countries
        /// <summary>
        /// Get a list of countries.
        /// </summary>
        /// <returns>Returns a list of countries.</returns>
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            _logger.LogInformation("Countries.Get called. Without arguments.");
            var countries = _service.GetList();

            _logger.LogTrace("Countries.Get ended. Return all Countries.");
            return countries;
        }

        // GET: api/Countries/5
        /// <summary>
        /// Get the country by id.
        /// </summary>
        /// <param name="id">Country id.</param>
        /// <returns>Returns a single country.</returns>
        [HttpGet("{id}")]
        public Country Get(int id)
        {
            _logger.LogInformation($"Countries.Get called. Arguments: Id = {id}.");
            var country = _service.GetItem(id);

            _logger.LogTrace($"Countries.Get ended. Return Country with Id = {id}.");
            return country;
        }

        // POST: api/Countries
        /// <summary>
        /// Create a country and stores it in the database.
        /// </summary>
        /// <param name="country">Data to create a country.</param>
        [HttpPost]
        public void Post([FromBody]CountryInput country)
        {
            if (country == null)
            {
                _logger.LogError("Countries.Post. Argument \"country\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Countries.Post called. Arguments: country Name = {country.Name}.");

            var newCountry = country.ConvertToCountry(country);

            if (_service.IsValid(newCountry))
            {
                _service.Create(newCountry);
                _logger.LogTrace("Countries.Post ended. Country successfully created.");
            }
            else
            {
                _logger.LogError("Countries.Post. Argument: country Name is invalid.");
                throw new ArgumentException();
            }
        }

        // PUT: api/Countries/5
        /// <summary>
        /// Update (HTTP PUT) an existing country with new data.
        /// </summary>
        /// <param name="id">Country id.</param>
        /// <param name="country">Data to update the country.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]CountryInput country)
        {
            if (country == null)
            {
                _logger.LogError("Countries.Put. Argument \"country\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Countries.Put called. Arguments: Id = {id}, country Name = {country.Name}.");

            var updatableCountry = country.ConvertToCountry(country);
            updatableCountry.Id = id;

            if (_service.IsValid(updatableCountry))
            {                
                _service.Update(id, updatableCountry);
                _logger.LogTrace("Countries.Put ended. Country successfully updated.");
            }
            else
            {
                _logger.LogError("Countries.Put. Argument: country Name is invalid.");
                throw new ArgumentException();
            }
        }
        
        // DELETE: api/Countries/5
        /// <summary>
        /// Delete the country by id.
        /// </summary>
        /// <param name="id">Country id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Countries.Delete called. Arguments: Id = {id}.");

            _service.Delete(id);
            _logger.LogTrace("Countries.Delete ended. Country successfully deleted.");
        }
    }
}
