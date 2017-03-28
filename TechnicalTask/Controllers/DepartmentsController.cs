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

        public DepartmentsController(DepartmentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Departments
        /// <summary>
        /// Get a list of departments. 
        /// </summary>
        /// <returns>Returns a list of departments.</returns>
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            var departments = _repository.GetList();
            return departments;
        }

        // GET: api/Departments/5
        /// <summary>
        /// Get the department by id.
        /// </summary>
        /// <param name="id">Department id.</param>
        /// <returns>Returns a single department.</returns>
        [HttpGet("{id}")]
        public Department Get(int id)
        {
            var department = _repository.GetItem(id);
            return department;
        }

        // POST: api/Departments
        /// <summary>
        /// Create a department and stores it in the database.
        /// </summary>
        /// <param name="department">Data to create a department.</param>
        [HttpPost]
        public void Post([FromBody]Department department)
        {
            _repository.Create(department);
        }

        // PUT: api/Departments/5
        /// <summary>
        /// Update (HTTP PUT) an existing department with new data.
        /// </summary>
        /// <param name="id">Department id.</param>
        /// <param name="department">Data to update the department.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Department department)
        {
            _repository.Update(id, department);
        }

        // DELETE: api/Departments/5
        /// <summary>
        /// Delete the department by id.
        /// </summary>
        /// <param name="id">Department id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
