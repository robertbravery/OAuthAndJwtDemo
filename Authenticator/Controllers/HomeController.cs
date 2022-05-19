using Authenticator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authenticator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("No Authentication Needed");

        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            var user = new UserLogins()
            {
                UserName = userName,
                Password = password
            };
            //new AuthenticationController().GetToken(user);
            var x = RedirectToAction("GetToken", "Authentication", user);
            //return Ok(x);
            return x;

        }

        [HttpGet]
        [Authorize]
        public IActionResult Secret()
        {
            return Ok("This is a secret");
        }
    }
}
