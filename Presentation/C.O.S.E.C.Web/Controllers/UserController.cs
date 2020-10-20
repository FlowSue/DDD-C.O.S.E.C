using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using C.O.S.E.C.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace C.O.S.E.C.Web.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        public UserController(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient();

        public IActionResult Login() => View();

        [Authorize]
        public IActionResult UserInfo() => View();

        [Authorize]
        public IActionResult WorkSchedule() => View();

        [Authorize]
        public IActionResult DailySchedule() => View();

        [Authorize]
        public IActionResult AddressBook() => View();

        public async Task<IActionResult> CheckLoginAsync(LoginEntity entity)
        {
            swaggerClient client = new swaggerClient("https://api.flowsue.top", _httpClient);
            entity.Os = 0;
            try
            {
                var result = await client.CheckLoginAsync(entity);
                HttpContext.Response.Cookies.Append("COSEC_TOKEN", result.Token, new CookieOptions
                {
                    Path = "/",
                    Expires = DateTime.Now.AddMinutes(20)
                });
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,entity.Username)
                };

                var Identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                Identity.AddClaims(claims);

                //init the identity instances 
                var userPrincipal = new ClaimsPrincipal(Identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.Now.AddMinutes(20),
                    IsPersistent = true,
                    AllowRefresh = true
                });
                return Json(new ResponseObject { Code = Domain.Enums.ResponseCode.OK, Data = result });
            }
            catch (Exception)
            {
                return Json(new ResponseObject { Code = Domain.Enums.ResponseCode.BadRequest, Info = "请联系管理员" });
            }
        }

        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Json(new ResponseObject { Code = Domain.Enums.ResponseCode.OK, Info = "退出成功" });
        }
    }
}
