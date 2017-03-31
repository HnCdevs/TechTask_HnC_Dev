//using System.IO;
//using System.Threading;
//using System.Threading.Tasks;
//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Auth.OAuth2.Flows;
//using Google.Apis.Auth.OAuth2.Web;
//using Google.Apis.Util.Store;
//using Microsoft.AspNetCore.Mvc;
//using Google.Apis.Plus.v1;
//using Google.Apis.Services;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting.Internal;
//using Microsoft.AspNetCore.Http;

//namespace TechnicalTask.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/Login")]
//    public class LoginController : Controller
//    {
//        private readonly string[] _scopes;
//        private readonly IHostingEnvironment _environment;

//        public LoginController()
//        {
//            _scopes = new [] { PlusService.Scope.PlusLogin };
//            _environment = new HostingEnvironment();
//        }      

//        [HttpPost]
//        public void Login([FromBody] JsonCredentialParameters json)
//        {
//            var cs = new ClientSecrets
//            {
//                ClientId = json.ClientId,
//                ClientSecret = json.ClientSecret
//            };
//            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(cs, _scopes, cs.ClientId, CancellationToken.None, new FileDataStore(@"folder", true)).Result;

//            HttpContext.Session.Set("accessToken", byteArray);
//            HttpContext.Authentication.SignInAsync()
//            //HttpContext.Session.TryGetValue("key", out data);
//        }

//        //public async Task IndexAsync(CancellationToken cancellationToken)
//        //{
//        //    var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata()).
//        //        AuthorizeAsync(cancellationToken);

//        //    if (result.Credential != null)
//        //    {
//        //        var service = new DriveService(new BaseClientService.Initializer
//        //        {
//        //            HttpClientInitializer = result.Credential,
//        //            ApplicationName = "ASP.NET MVC Sample"
//        //        });

//        //        // YOUR CODE SHOULD BE HERE..
//        //        // SAMPLE CODE:
//        //        var list = await service.Files.List().ExecuteAsync();
//        //        ViewBag.Message = "FILE COUNT IS: " + list.Items.Count();
//        //        return View();
//        //    }
//        //    else
//        //    {
//        //        return new RedirectResult(result.RedirectUri);
//        //    }
//        //}
//    }
//}