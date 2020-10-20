using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 报表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
    public class ReportController : ControllerBase
    {
        public ReportController()
        {
        }

        [HttpPost]
        public void Test()
        {
            //throw new NotImplementedException();
        }
    }
}
