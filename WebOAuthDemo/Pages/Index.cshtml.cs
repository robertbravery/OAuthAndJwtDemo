using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;
using System.Net.Http.Headers;

namespace WebOAuthDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetOnClick()
        {
            var url = $"https://localhost:7203/api/Home/Secret";
            var token = HttpContext.Request.Cookies["token"];
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    
                    using (var response = httpClient.SendAsync(request).GetAwaiter().GetResult())
                    {
                        //httpClient.GetAsync(url).GetAwaiter().GetResult()
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            return RedirectToPage("Secret");
                        }
                        else
                        {
                            return RedirectToPage("Error");
                        }
                    }
                }
            }
        }
    }
}