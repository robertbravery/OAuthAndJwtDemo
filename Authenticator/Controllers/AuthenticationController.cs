using Authenticator.Helpers;
using Authenticator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Authenticator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        public AuthenticationController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        [HttpPost]
        public IActionResult Authenticate(UserLogins userLogins)
        {

            return GetToken(userLogins.UserName, userLogins.Password);
        }

        [HttpPost]
        public IActionResult GetToken(string userName, string password)
        {
            try
            {
                User user = new User();
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.PostAsync($"https://localhost:7016/api/Authenticate/GetUser?userName={userName}&password={password}", null).GetAwaiter().GetResult())
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            user = JsonConvert.DeserializeObject<User>(apiResponse);
                        }
                        else
                        {
                            return Unauthorized();
                        }

                        if (user == null)
                        {
                            return Unauthorized();
                        }
                    }
                }

                var Token = new UserTokens();

                Token = JwtHelpers.GenTokenkey(new UserTokens()
                {
                    //Pass in any extra information needed
                    EmailId = user.EmailId,
                    GuidId = Guid.NewGuid(),
                    UserName = user.UserName,
                    Id = user.Id,
                }, jwtSettings);
                
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
