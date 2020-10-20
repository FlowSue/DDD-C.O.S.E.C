using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        public IActionResult Requisition() => View();
        public IActionResult Dispatch() => View();
        public IActionResult Charge() => View();
        public IActionResult Returnvisit() => View();
        public IActionResult Complaint() => View();
    }
}
