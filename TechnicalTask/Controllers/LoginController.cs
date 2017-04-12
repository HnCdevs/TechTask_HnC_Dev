using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalTask.Models;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("/LoggedIn")]
        public string GetLoggedIn()
        {
            return "Login successful";
        }

        [HttpGet]
        [Route("/LoggedOut")]
        public string GetLoggedOut()
        {
            return "Logout successful";
        }
    }

}