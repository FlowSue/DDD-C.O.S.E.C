using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        public IActionResult Plan() => View();

        public IActionResult Information() => View();

        public IActionResult Forecast() => View();

        public IActionResult Quote() => View();

        public IActionResult OrderManagement() => View();

        public IActionResult ContractManagement() => View();

        public IActionResult SalesTarget() => View();
    }
}
