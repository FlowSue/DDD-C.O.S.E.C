using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Web.Controllers
{
    [Authorize]
    public class SystemController : Controller
    {
        public IActionResult Certification() => View();
        public IActionResult UserGroup() => View();
        public IActionResult Organization() => View();
        public IActionResult PermissionSettings() => View();
        public IActionResult UserManagement() => View();
        public IActionResult ProcessSettings() => View();
        public IActionResult PolicySetting() => View();
        public IActionResult SystemLog() => View();
        public IActionResult SystemStatus() => View();
    }
}
