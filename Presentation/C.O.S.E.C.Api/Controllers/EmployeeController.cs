using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 员工系统接口
    /// </summary>
    [Route("api/[action]")]
    [ApiController]
    [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeWorkLogBLL workLogBLL;
        private readonly IEmployeeAttendanceBLL attendanceBLL;
        private readonly IEmployeeWorkPlanBLL workPlanBLL;

        public EmployeeController(IEmployeeWorkLogBLL workLogBLL, IEmployeeAttendanceBLL attendanceBLL, IEmployeeWorkPlanBLL workPlanBLL)
        {
            this.workLogBLL = workLogBLL;
            this.attendanceBLL = attendanceBLL;
            this.workPlanBLL = workPlanBLL;
        }

        #region 员工考勤

        #endregion

        #region 工作计划
        /// <summary>
        /// 获取工作计划分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet, Description("获取工作计划列表")]
        public async Task<PagingResult<EmployeeWorkPlan>> GetWorkPlanPageAsync(Pagination pagination)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }

            SqlSugar.RefAsync<int> totalNumber = default;
            var list = await workPlanBLL.GetPageListAsync(n => n.IsDelete == false && n.IsEnable == true && n.Status == StatusState.Normal, pagination, totalNumber).ConfigureAwait(false);
            pagination.Records = totalNumber.Value;
            return new PagingResult<EmployeeWorkPlan>(pagination) { Data = list };
        }

        /// <summary>
        /// 获取工作计划内容
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet, Description("读取工作计划详情")]
        public async Task<EmployeeWorkPlan> GetWorkPlanAsync(Guid keyValue) => await workPlanBLL.GetEntityAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 新增工作计划
        /// </summary>
        /// <param name="workPlan"></param>
        /// <returns></returns>
        [HttpPost, Description("新增工作计划")]
        public async Task<bool> WriteWorkPlanAsync(EmployeeWorkPlan workPlan) => await workPlanBLL.SaveFormAsync(Guid.Empty, workPlan).ConfigureAwait(false);

        /// <summary>
        /// 修改工作计划
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="workPlan"></param>
        /// <returns></returns>
        [HttpPut, Description("修改工作计划")]
        public async Task<bool> EditWorkPlanAsync(Guid keyValue, EmployeeWorkPlan workPlan) => await workPlanBLL.SaveFormAsync(keyValue, workPlan).ConfigureAwait(false);

        /// <summary>
        /// 删除工作计划
        /// </summary>
        /// <param name="keyValue"></param>
        [HttpDelete, Description("删除工作计划")]
        public async Task DeleteWorkPlanAsync(Guid keyValue) => await workLogBLL.DeleteAsync(keyValue).ConfigureAwait(false);
        #endregion

        #region 工作日志
        /// <summary>
        /// 获取工作日志分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet, Description("获取工作日志列表")]
        public async Task<PagingResult<EmployeeWorkLog>> GetWorkLogPageAsync(Pagination pagination)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }

            SqlSugar.RefAsync<int> totalNumber = default;
            var list = await workLogBLL.GetPageListAsync(n => n.IsDelete == false && n.IsEnable == true && n.Status == StatusState.Normal, pagination, totalNumber).ConfigureAwait(false);
            pagination.Records = totalNumber.Value;
            return new PagingResult<EmployeeWorkLog>(pagination) { Data = list };
        }

        /// <summary>
        /// 获取工作日志内容
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet, Description("读取工作日志详情")]
        public async Task<EmployeeWorkLog> GetWorkLogAsync(Guid keyValue) => await workLogBLL.GetEntityAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 新增工作日志
        /// </summary>
        /// <param name="workLog"></param>
        /// <returns></returns>
        [HttpPost, Description("新增工作日志")]
        public async Task<bool> WriteWorkLogAsync(EmployeeWorkLog workLog) => await workLogBLL.SaveFormAsync(Guid.Empty, workLog).ConfigureAwait(false);

        /// <summary>
        /// 修改工作日志
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="workLog"></param>
        /// <returns></returns>
        [HttpPut, Description("修改工作日志")]
        public async Task<bool> EditWorkLogAsync(Guid keyValue, EmployeeWorkLog workLog) => await workLogBLL.SaveFormAsync(keyValue, workLog).ConfigureAwait(false);

        /// <summary>
        /// 删除工作日志
        /// </summary>
        /// <param name="keyValue"></param>
        [HttpDelete, Description("删除工作日志")]
        public async Task DeleteWorkLogAsync(Guid keyValue) => await workLogBLL.DeleteAsync(keyValue).ConfigureAwait(false);
        #endregion
    }
}