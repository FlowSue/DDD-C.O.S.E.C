using Microsoft.AspNetCore.Mvc;
using System;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 用户功能列表管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public MenuController() { }

        [HttpGet] public dynamic GetUserMenu() => throw new NotImplementedException();
    }
}