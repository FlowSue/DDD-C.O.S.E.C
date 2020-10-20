using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 日志
    /// </summary>
    [Route("api/[controller]")]
    [ApiController, Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
    public class LogController : ControllerBase
    {
        private readonly _ISystemActionLogBLL logBLL;

        public LogController(_ISystemActionLogBLL logBLL)
        {
            this.logBLL = logBLL;
        }

        [HttpGet, Description("获取日志列表")]
        public async Task<PagingResult<_SystemActionLog>> GetLoggingAsync([FromQuery] Pagination pagination)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }

            SqlSugar.RefAsync<int> totalNum = 0;
            var list = await logBLL.GetPageListAsync(n => n.IsDelete == false && n.Status == StatusState.Normal, pagination, totalNum).ConfigureAwait(false);
            pagination.Records = totalNum.Value;
            return new PagingResult<_SystemActionLog>(pagination) { Data = list };
        }
    }
}