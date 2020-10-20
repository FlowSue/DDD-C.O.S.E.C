using C.O.S.E.C.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> Index()
        {
            swaggerClient client = new swaggerClient("https://api.flowsue.top", _httpClient);
            HttpContext.Request.Cookies.TryGetValue("COSEC_TOKEN", out var token);
            var result = await client.SerializeJWTAsync(token?.Split(' ').LastOrDefault() ?? "");
            HttpContext.Response.Cookies.Append("COSEC_USER", result.Uid);
            HttpContext.Response.Cookies.Append("COSEC_SYSTEM", result.SystemId);
            HttpContext.Response.Cookies.Append("COSEC_UNAME", result.Uname);
            HttpContext.Response.Cookies.Append("COSEC_RNAME", result.Rname);
            return View(result);
        }
        public IActionResult Console() => View();
        public IActionResult HomePage1() => View();
        public IActionResult HomePage2() => View();
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
