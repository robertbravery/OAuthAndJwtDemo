using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebOAuthDemo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebOAuthDemo.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        [BindProperty]
        public UserLogins UserLogins { get; set; } = default!;
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger; 
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.PostAsync($"https://localhost:7203/api/Authentication/GetToken?userName={UserLogins.UserName}&password={UserLogins.Password}", null).GetAwaiter().GetResult())
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var jwt = JObject.Parse(apiResponse);
                        HttpContext.Response.Cookies.Append("token", jwt["token"].ToString(),
                        new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(30) });
                        return RedirectToPage("Index");

                    }
                    else
                    {
                        RedirectToPage("Error");
                    }
                }
            }
            return RedirectToPage("Index");
        }
    }

}