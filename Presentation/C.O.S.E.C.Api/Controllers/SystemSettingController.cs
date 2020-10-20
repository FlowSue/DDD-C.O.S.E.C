using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [Route("api/[action]")]
    [ApiController]
    public class SystemSettingController : ControllerBase
    {
        public SystemSettingController() { }

        [HttpGet] public Task<object> GetSetting() => throw new NotImplementedException();
        [HttpPut] public Task<object> SetSetting() => throw new NotImplementedException();
        [HttpDelete] public Task<object> DelSetting() => throw new NotImplementedException();
    }
}