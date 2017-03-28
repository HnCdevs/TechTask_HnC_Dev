using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repository;
        private readonly ILogger _logger;

        public UsersController(IRepository<User> repository, ILoggerFactory logger)
        {
            _repository = repository;
            _logger = logger.CreateLogger("User Controller");
        }

        // GET: api/Users
        /// <summary>
        /// Get a list of users. 
        /// </summary>
        /// <returns>Returns a list of users.</returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = _repository.GetList();
            _logger.LogError("awgwagwagwagawg");
            return users;
        }

        // GET api/Users/5
        /// <summary>
        /// Get the user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Returns a single user.</returns>
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = _repository.GetItem(id);
            return user;
        }

        // POST api/Users
        /// <summary>
        /// Create a user and stores it in the database.
        /// </summary>
        /// <param name="user">Data to create a user.</param>
        [HttpPost]
        public void Post([FromBody]User user)
        {
            _repository.Create(user);
        }

        // PUT api/Users/5
        /// <summary>
        /// Update (HTTP PUT) an existing user with new data.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="user">Data to update the user.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User user)
        {
            _repository.Update(id, user);
        }

        // DELETE api/Users/5
        /// <summary>
        /// Delete the user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
