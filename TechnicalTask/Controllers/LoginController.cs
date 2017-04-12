using Microsoft.AspNetCore.Mvc;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    public class LoginController : Controller
    {
        /// <summary>
        /// Get login result.
        /// </summary>
        /// <returns>Returns result string.</returns>
        [HttpGet]
        [Route("/LoggedIn")]
        public string GetLoggedIn()
        {
            return "Login successful";
        }

        /// <summary>
        /// Get logout result.
        /// </summary>
        /// <returns>Returns result string.</returns>
        [HttpGet]
        [Route("/LoggedOut")]
        public string GetLoggedOut()
        {
            return "Logout successful";
        }
    }
}