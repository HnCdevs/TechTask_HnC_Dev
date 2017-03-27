using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Departments")]
    public class DepartmentsController : Controller
    {
        private readonly IRepository<Department> _repository;

        public DepartmentsController(IRepository<Department> repository)
        {
            _repository = repository;
        }

        // GET: api/Departments
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            var departments = _repository.GetList();
            return departments;
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            var department = _repository.GetItem(id);
            return department;
        }
        
        // POST: api/Departments
        [HttpPost]
        public void Post([FromBody]Department department)
        {
            _repository.Create(department);
        }
        
        // PUT: api/Departments
        [HttpPut]
        public void Put(int id, [FromBody]Department department)
        {
            _repository.Update(id, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
