using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechnicalTask.Models;
using TechnicalTask.Services;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TreeController : Controller
    {
        private readonly IService<Organization> _service;
        private readonly ILogger _logger;

        public TreeController(IService<Organization> service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger("Tree Controller");
        }

        // GET: api/Tree
        /// <summary>
        /// Get the tree.
        /// </summary>
        /// <returns>Returns whole tree.</returns>
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            _logger.LogInformation("Tree.Get called. Without arguments.");
            var tree = _service.GetList();

            _logger.LogTrace("Tree.Get ended. Return whole tree.");
            return tree;
        }      
    }
}
