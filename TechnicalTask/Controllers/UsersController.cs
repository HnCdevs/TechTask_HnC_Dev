using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repository;

        public UsersController(IRepository<User> repository)
        {
            _repository = repository;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var users = _repository.GetList();
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

        // PUT api/Users/5
        [HttpPut("{id}")]
        public void Put([FromBody]User user)
        {
            _repository.Update(user);
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
