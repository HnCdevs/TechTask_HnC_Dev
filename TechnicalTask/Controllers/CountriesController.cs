using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Countries")]
    public class CountriesController : Controller
    {
        private readonly CountryRepository _repository;

        public CountriesController(CountryRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Countries
        /// <summary>
        /// Get a list of countries.
        /// </summary>
        /// <returns>Returns a list of countries.</returns>
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            var countries = _repository.GetList();
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
            var country = _repository.GetItem(id);
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
                throw new ArgumentNullException();
            }
            var newCountry = country.ConvertToCountry(country);

            if (_repository.IsValid(newCountry))
            {
                _repository.Create(newCountry);
            }
            else
            {
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
                throw new ArgumentNullException();
            }
            var updatableCountry = country.ConvertToCountry(country);

            if (_repository.IsValid(updatableCountry))
            {
                _repository.Update(id, updatableCountry);
            }
            else
            {
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
            _repository.Delete(id);
        }
    }
}
