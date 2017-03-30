using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Logs")]
    public class LogsController : Controller
    {
        private readonly ILogger _logger;
        public LogsController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LogsLogger");
        }

        // GET: api/Logs
        /// <summary>
        /// Get a Logs
        /// </summary>
        /// <returns>Returns a Logs.</returns>
        [HttpGet]
        public string Get()
        {
            var files = Directory.GetFiles("Logs/");

            FileInfo latest = null;
            DateTime latesDate = DateTime.MinValue;

            foreach (var file in files)
            {
                FileInfo f = new FileInfo(file);
                if (f.LastWriteTime > latesDate)
                {
                    latest = f;
                }
            }

            if (latest == null)
                return "No Logs available";

            _logger.LogInformation("Returns logs from " + latest.Name);

            string logs;

            using (var fs = new FileStream(latest.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs))
            {
                logs = sr.ReadToEnd();
            }
            return logs;
        }
    }
}