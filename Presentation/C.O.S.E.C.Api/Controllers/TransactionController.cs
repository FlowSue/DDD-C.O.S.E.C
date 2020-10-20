using Microsoft.AspNetCore.Mvc;
using System;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 交易管理
    /// </summary>
    [Route("api/[action]")]
    public class TransactionController : ControllerBase
    {
        #region Order
        [HttpGet] public dynamic GetOrder() => throw new NotImplementedException();
        [HttpPost] public dynamic AddOrder() => throw new NotImplementedException();
        [HttpPut] public dynamic EditOrder() => throw new NotImplementedException();
        [HttpDelete] public dynamic DeleteOrder() => throw new NotImplementedException();
        #endregion

        #region Contract
        [HttpGet] public dynamic GetContract() => throw new NotImplementedException();
        [HttpPost] public dynamic AddContract() => throw new NotImplementedException();
        [HttpPut] public dynamic EditContract(Guid keyValue) => throw new NotImplementedException();
        [HttpDelete] public dynamic DeleteContract(Guid keyValue) => throw new NotImplementedException();
        #endregion
    }
}
