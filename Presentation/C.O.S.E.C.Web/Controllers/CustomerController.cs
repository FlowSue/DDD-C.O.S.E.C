using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        public IActionResult Highseas() => View();

        public IActionResult Mine() => View();

        public IActionResult Followup() => View();

    }
}
