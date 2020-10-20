using C.O.S.E.C.Domain.Entity.Internal;
using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.FactoryRepository;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using C.O.S.E.C.Infrastructure.Config;
using C.O.S.E.C.Infrastructure.CustomException;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 内部用
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthPolicyEnum.RequireRoleOfAdmin)]
    public class InternalController : ControllerBase
    {
        private readonly AllConfigModel _allConfigModel;
        private readonly IEntityBaseAutoSetter _setter;
        public InternalController(AllConfigModel allConfigModel, IEntityBaseAutoSetter setter)
        {
            _allConfigModel = allConfigModel;
            _setter = setter;
        }
        #region 目标客户&商机客户
        /// <summary>
        /// 获取目标客户
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/[controller]/{type}/[action]"), Description("获取目标客户")]
        public async Task<List<TargetCustomers>> GetTargetCustomersAsync(int type) => await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Queryable<TargetCustomers>().Where(n => n.IsDelete == false && n.ClientType == type).ToListAsync().ConfigureAwait(false);
        /// <summary>
        /// 新增目标客户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost, Description("新增目标客户")]
        public async Task<TargetCustomers> CreateTargetCustomerAsync([FromBody] TargetCustomers info) => await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Insertable<TargetCustomers>(info.Create(_setter)).ExecuteReturnEntityAsync().ConfigureAwait(false);
        /// <summary>
        /// 修改目标客户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut("/api/[controller]/{id}/[action]"), Description("修改目标客户")]
        public async Task<bool> EditTargetCustomerAsync(int id, [FromBody] TargetCustomers info)
        {
            if (!id.IsNullOrEmpty())
            {
                var entity = await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Queryable<TargetCustomers>().InSingleAsync(id).ConfigureAwait(false);
                var result = await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Updateable<TargetCustomers>(info.Modify(id, _setter)).Where(n => n.ID == id).IgnoreColumns(true).ExecuteCommandHasChangeAsync().ConfigureAwait(false);
                return result;
            }
            throw new AppException("ID不能为空");
        }
        /// <summary>
        /// 删除目标客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/[controller]/{id}/[action]"), Description("删除目标客户")]
        public async Task<bool> DeleteTargetCustomerAsync(int id) => await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Updateable<TargetCustomers>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == id).ExecuteCommandHasChangeAsync().ConfigureAwait(false);
        #endregion

        #region 成交客户
        /// <summary>
        /// 获取成交客户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/[controller]/{type}/[action]"), Description("获取成交客户列表")]
        public async Task<List<InternalCustomers>> GetInternalCustomersAsync(int type) => await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Queryable<InternalCustomers>().Where(n => n.IsDelete == false).WhereIF(!(~type).IsNullOrEmpty(), n => n.ClientType == type).WhereIF((~type).IsNullOrEmpty(), n => SqlFunc.DateAdd(DateTime.Now, 60) >= n.ServiceExpired).ToListAsync().ConfigureAwait(false);
        /// <summary>
        /// 新增成交客户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost, Description("新增成交客户")]
        public async Task<InternalCustomers> CreateInternalCustomerAsync([FromBody] InternalCustomers info) => await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Insertable<InternalCustomers>(info.Create(_setter)).ExecuteReturnEntityAsync().ConfigureAwait(false);
        /// <summary>
        /// 修改成交客户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut("/api/[controller]/{id}/[action]"), Description("修改成交客户")]
        public async Task<bool> EditInternalCustomerAsync(int id, [FromBody] InternalCustomers info)
        {
            if (!id.IsNullOrEmpty())
            {
                var entity = await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Queryable<InternalCustomers>().InSingleAsync(id).ConfigureAwait(false);
                var result = await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Updateable<InternalCustomers>(info.Modify(id, _setter)).Where(n => n.ID == id).IgnoreColumns(true).ExecuteCommandHasChangeAsync().ConfigureAwait(false);
                return result;
            }
            throw new AppException("ID不能为空");
        }
        /// <summary>
        /// 删除成交客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/[controller]/{id}/[action]"), Description("删除成交客户")]
        public async Task<bool> DeleteInternalCustomerAsync(int id) => await DbConfig.GetDbInstance(_allConfigModel.ConnectionStringsModel.SqlServerDatabase).Updateable<InternalCustomers>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == id).ExecuteCommandHasChangeAsync().ConfigureAwait(false);
        #endregion
    }
}
