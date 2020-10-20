using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public IActionResult Archive() => View();
        public IActionResult Inventory() => View();
        public IActionResult Categories() => View();
        public IActionResult Incentives() => View();
    }
}
