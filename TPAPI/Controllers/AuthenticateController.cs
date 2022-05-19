using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace TPAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly User[] _users = new User[]
            {
                new User(){FullName = "Robert Bravery", ID = 1, Password="password", UserName="rbravery"},
                new User(){FullName = "Joe Soap", ID = 2, Password="password", UserName="jsoap"},
                new User(){FullName = "Jane Doe", ID = 2, Password="password", UserName="jdoe"}
            };

        [HttpPost()]
        public User GetUser(string userName, string password)
        {
            if (userName == null || password == null) 
                throw new ArgumentNullException("Invalid Credentials");
            var user =  _users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            
            return user;
        }
    }
}
