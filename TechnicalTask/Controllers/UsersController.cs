using System;
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
        private readonly UserRepository _repository;
        private readonly ILogger _logger;

        public UsersController(UserRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger("Users Controller");
        }

        // GET: api/Users
        /// <summary>
        /// Get a list of users. 
        /// </summary>
        /// <returns>Returns a list of users.</returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            _logger.LogInformation("Users.Get called. Without arguments.");
            var users = _repository.GetList();

            _logger.LogTrace("Users.Get ended. Return all Users.");
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
            _logger.LogInformation($"Users.Get called. Arguments: Id = {id}.");
            var user = _repository.GetItem(id);

            _logger.LogTrace($"Users.Get ended. Return User with Id = {id}.");
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
            if (user == null)
            {
                _logger.LogError("Users.Post. Argument \"user\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Users.Post called. Arguments: user Name = {user.Name}.");
           
            _repository.Create(user);
            _logger.LogTrace("Users.Post ended. User successfully created.");
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
            if (user == null)
            {
                _logger.LogError("Users.Put. Argument \"user\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Users.Put called. Arguments: Id = {id}, user Name = {user.Name}.");
            
            _repository.Update(id, user);
            _logger.LogTrace("Users.Put ended. User successfully updated.");
        }

        // DELETE api/Users/5
        /// <summary>
        /// Delete the user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Users.Delete called. Arguments: Id = {id}.");

            _repository.Delete(id);
            _logger.LogTrace("Users.Delete ended. User successfully deleted.");
        }
    }
}
