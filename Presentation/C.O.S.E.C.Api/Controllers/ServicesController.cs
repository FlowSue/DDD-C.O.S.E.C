using Microsoft.AspNetCore.Mvc;
using System;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 服务管理
    /// </summary>
    [Route("api/[action]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        public ServicesController() { }

        #region Customer Services

        #endregion

        #region after sales service
        [HttpGet] public dynamic ApplyService() => throw new NotImplementedException();
        [HttpGet] public dynamic GetWorkOrder(Guid keyValue) => throw new NotImplementedException();
        [HttpPost] public dynamic CreateWorkOrder() => throw new NotImplementedException();
        [HttpPut] public dynamic EditWorkOrder(Guid keyValue) => throw new NotImplementedException();
        [HttpDelete] public dynamic DeleteWorkOrder(Guid keyValue) => throw new NotImplementedException();

        #endregion

        #region technological service

        #endregion
    }
}