using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = _repository.GetList();
            _logger.LogError("awgwagwagwagawg");
            return users;
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var user = _repository.GetItem(id);
            return user;
        }

        // POST api/Users
        [HttpPost]
        public void Post([FromBody]User user)
        {
            _repository.Create(user);
        }

        // PUT api/Users
        [HttpPut]
        public void Put(int id, [FromBody]User user)
        {
            _repository.Update(id, user);
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
