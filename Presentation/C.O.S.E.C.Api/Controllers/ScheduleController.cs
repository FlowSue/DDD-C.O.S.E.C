using Microsoft.AspNetCore.Mvc;
using System;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 日程管理
    /// </summary>
    [Route("api/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        public ScheduleController()
        {
        }
        [HttpGet] public dynamic GetSchedule() => new NotImplementedException();
        [HttpPost] public dynamic AddSchedule() => throw new NotImplementedException();
        [HttpPut] public dynamic EditSchedule() => throw new NotImplementedException();
        [HttpDelete] public dynamic DeleteSchedule() => throw new NotImplementedException();
        [HttpPost] public dynamic ScheduleReminder() => throw new NotImplementedException();
        [HttpPost] public dynamic ScheduleNotice() => throw new NotImplementedException();
    }
}