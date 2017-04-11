using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Plus.v1;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TechnicalTask.Models;

namespace TechnicalTask.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        //private readonly string[] _scopes;
        //private readonly IHostingEnvironment _environment;
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;

        //public LoginController(UserManager<User> userManager,
        //    SignInManager<User> signInManager)
        //{
        //    //_scopes = new[] { PlusService.Scope.PlusLogin };
        //    //_environment = new HostingEnvironment();
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        //public void Login(string provider, string returnUrl = null)
        //{
        //    var redirectUrl = Url.Action("https://console.developers.google.com/projectselector/apis/library");
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    //return Challenge(properties, provider);
        //}
        //[HttpPost]
        //public void Login([FromBody] JsonCredentialParameters json)
        //{
        //    var cs = new ClientSecrets
        //    {
        //        ClientId = json.ClientId,
        //        ClientSecret = json.ClientSecret
        //    };
        //    var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(cs, _scopes, cs.ClientId, CancellationToken.None, new FileDataStore(@"folder", true)).Result;

        //    HttpContext.Session.Set("accessToken", byteArray);
        //    HttpContext.Authentication.SignInAsync()
        //    //HttpContext.Session.TryGetValue("key", out data);
        //}

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult ExternalLogin(string provider, string returnUrl = null)
        //{
        //    // Request a redirect to the external login provider.
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Login", new { ReturnUrl = returnUrl });
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return Challenge(properties, provider);
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //    if (remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
        //        //return View(nameof(Login));
        //    }
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return null; //RedirectToAction(nameof(Login));
        //    }

        //    // Sign in the user with this external login provider if the user already has a login.
        //    var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        //    if (result.Succeeded)
        //    {
        //        //_logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
        //        return RedirectToRoute("api/swagger");
        //    }
        //    //if (result.RequiresTwoFactor)
        //    //{
        //    //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
        //    //}
        //    //if (result.IsLockedOut)
        //    //{
        //    //    return View("Lockout");
        //    //}
        //    //else
        //    //{
        //    //    // If the user does not have an account, then ask the user to create an account.
        //    //    ViewData["ReturnUrl"] = returnUrl;
        //    //    ViewData["LoginProvider"] = info.LoginProvider;
        //    //    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //    //    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
        //    //}
        //    return null;
        //}
    }
}