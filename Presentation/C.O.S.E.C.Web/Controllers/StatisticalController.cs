using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class StatisticalController : Controller
    {
        public IActionResult Index() => View();
    }
}
