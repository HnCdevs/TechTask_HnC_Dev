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
    [Route("api/Departments")]
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IService<Department> _service;
        private readonly ILogger _logger;

        public DepartmentsController(IService<Department> service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger("Departments Controller");
        }

        // GET: api/Departments
        /// <summary>
        /// Get a list of departments. 
        /// </summary>
        /// <returns>Returns a list of departments.</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Department> Get()
        {
            _logger.LogInformation("Departments.Get called. Without arguments.");
            var departments = _service.GetList();

            _logger.LogTrace("Departments.Get ended. Return all Departments.");
            return departments;
        }

        // GET: api/Departments/5
        /// <summary>
        /// Get the department by id.
        /// </summary>
        /// <param name="id">Department id.</param>
        /// <returns>Returns a single department.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public Department Get(int id)
        {
            _logger.LogInformation($"Departments.Get called. Arguments: Id = {id}.");
            var department = _service.GetItem(id);

            _logger.LogTrace($"Departments.Get ended. Return Department with Id = {id}.");
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
            if (department == null)
            {
                _logger.LogError("Departments.Post. Argument \"department\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Departments.Post called. Arguments: department Name = {department.Name}.");

            if (_service.IsValid(department))
            {
                _service.Create(department);
                _logger.LogTrace("Departments.Post ended. Department successfully created.");
            }
            else
            {
                _logger.LogError("Departments.Post. Argument: department Name is invalid.");
                throw new ArgumentException();
            }
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
            if (department == null)
            {
                _logger.LogError("Departments.Put. Argument \"department\" is null.");
                throw new ArgumentNullException();
            }
            _logger.LogInformation($"Departments.Put called. Arguments: Id = {id}, department Name = {department.Name}.");

            if (_service.IsValid(department))
            {
                _service.Update(id, department);
                _logger.LogTrace("Departments.Put ended. Department successfully updated.");
            }
            else
            {
                _logger.LogError("Departments.Put. Argument: department Name is invalid.");
                throw new ArgumentException();
            }
        }

        // DELETE: api/Departments/5
        /// <summary>
        /// Delete the department by id.
        /// </summary>
        /// <param name="id">Department id.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Departments.Delete called. Arguments: Id = {id}.");

            _service.Delete(id);
            _logger.LogTrace("Departments.Delete ended. Department successfully deleted.");
        }
    }
}
