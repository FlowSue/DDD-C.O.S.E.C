using C.O.S.E.C.Api.Properties;
using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using C.O.S.E.C.Infrastructure.Treasury.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 商机<![CDATA[&]]>客户系统接口
    /// </summary>
    [Route("api/[action]")]
    [ApiController]
    [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
    public class CustomerController : ControllerBase
    {
        private readonly ICluePoolBLL cluePoolBLL;
        private readonly IBusinessPoolBLL businessPoolBLL;
        private readonly ICustomerBLL customerBLL;
        private readonly ICustomerTrailRecordBLL trailRecordBLL;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cluePoolBLL"></param>
        /// <param name="businessPoolBLL"></param>
        /// <param name="customerBLL"></param>
        /// <param name="trailRecordBLL"></param>
        public CustomerController(ICluePoolBLL cluePoolBLL, IBusinessPoolBLL businessPoolBLL, ICustomerBLL customerBLL, ICustomerTrailRecordBLL trailRecordBLL)
        {
            this.cluePoolBLL = cluePoolBLL;
            this.businessPoolBLL = businessPoolBLL;
            this.customerBLL = customerBLL;
            this.trailRecordBLL = trailRecordBLL;
        }

        #region 线索池
        /// <summary>
        /// 获取线索数据
        /// </summary>
        /// <returns></returns>
        [HttpGet, Description("获取线索列表")]
        public async Task<List<CluePool>> GetCluesAsync() => await cluePoolBLL.GetListAsync(n => n.IsDelete == false).ConfigureAwait(false);

        /// <summary>
        /// 获取线索分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet, Description("获取线索分页列表")]
        public async Task<PagingResult<CluePool>> GetCluePageAsync([FromQuery] Pagination pagination)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }
            SqlSugar.RefAsync<int> totalNumber = 0;
            var list = await cluePoolBLL.GetPageListAsync(n => n.IsDelete == false && n.Status == StatusState.Normal && n.IsEnable == true, pagination, totalNumber).ConfigureAwait(false);
            pagination.Records = totalNumber.Value;
            return new PagingResult<CluePool>(pagination) { Data = list };
        }

        /// <summary>
        /// Excel导入线索
        /// </summary>
        /// <returns></returns>
        [HttpGet, Description("Excel导入线索")]
        public async Task<bool> ExportExcelAsync(IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment, [FromServices] IEntityBaseAutoSetter setter)
        {
            //var fileBase = HttpContext.Request.Form.Files[0];
            var size = file.Length;
            if (size > 1024 * 1024 * 3)
            {
                throw new Infrastructure.CustomException.AppException(Resources.Error_txt_img_size);
            }
            var extname = file.FileName.Split(".").Last();
            var fileName = $"{setter.SystemId}-{DateTime.Now.Ticks}.{extname}";
            var fileNamePath = $@"{hostingEnvironment.ContentRootPath}\UploadFile\ExportFile\{DateTime.Today:d}\{fileName}";
            if (!Directory.Exists($@"{hostingEnvironment.ContentRootPath}\UploadFile\ExportFile\{DateTime.Today:d}\"))
            {
                _ = Directory.CreateDirectory($@"{hostingEnvironment.ContentRootPath}\UploadFile\ExportFile\{DateTime.Today:d}\");//不存在就创建目录
            }
            using FileStream fs = System.IO.File.Create(fileNamePath);
            await file.CopyToAsync(fs).ConfigureAwait(false);
            fs.Flush();
            //调用ExcelHelper方法
            DataSet ds = ExcelHeler.ReadExcelToDataSet(fileNamePath);
            return await cluePoolBLL.RangeAsync(ds.Tables[0].ToList<CluePool>()).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取线索详情
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet, Description("读取线索详情")]
        public async Task<CluePool> GetClueAsync(Guid keyValue) => await cluePoolBLL.GetEntityAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 新增线索
        /// </summary>
        /// <param name="clue"></param>
        /// <returns></returns>
        [HttpPost, Description("新增线索")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<bool> AddClueAsync(CluePool clue) => await cluePoolBLL.SaveFormAsync(default, clue).ConfigureAwait(false);

        /// <summary>
        /// 修改线索
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="clue"></param>
        /// <returns></returns>
        [HttpPut, Description("修改线索")]
        [Authorize(AuthPolicyEnum.RequireRoleOfAdmin)]
        public async Task<bool> EditClueAsync(Guid keyValue, CluePool clue) => await cluePoolBLL.SaveFormAsync(keyValue, clue).ConfigureAwait(false);

        /// <summary>
        /// 删除线索
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpDelete, Description("删除线索")]
        public async Task<bool> DeleteClueAsync(Guid keyValue) => await cluePoolBLL.DeleteAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 线索转换商机
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPut, Description("线索转换商机")]
        public async Task<bool> ConversionBusinessAsync(Guid keyValue) => await Task.Run(() => cluePoolBLL.ConversionBusiness(keyValue)).ConfigureAwait(false);
        #endregion

        #region 商机池
        /// <summary>
        /// 获取商机分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet, Description("获取商机列表")]
        public async Task<PagingResult<BusinessPool>> GetBusinessPageAsync([FromQuery] Pagination pagination)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }

            SqlSugar.RefAsync<int> totalNumber = default;
            var list = await businessPoolBLL.GetPageListAsync(n => n.IsDelete == false && n.Status == StatusState.Normal && n.IsEnable == true, pagination, totalNumber).ConfigureAwait(false);
            pagination.Records = totalNumber.Value;
            return new PagingResult<BusinessPool>(pagination) { Data = list };
        }

        /// <summary>
        /// 标记无效商机
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPut, Description("标记无效商机")]
        public async Task<bool> InvalidBusinessAsync(Guid keyValue) => await Task.Run(() => businessPoolBLL.Invalid(keyValue)).ConfigureAwait(false);

        /// <summary>
        /// 获取商机详情
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet, Description("读取商机详情")]
        public async Task<BusinessPool> GetBusinessAsync(Guid keyValue) => await businessPoolBLL.GetEntityAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 新增商机
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPost, Description("新增商机")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<bool> AddBusinessAsync(BusinessPool business) => await businessPoolBLL.SaveFormAsync(default, business).ConfigureAwait(false);

        /// <summary>
        /// 修改商机
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="business"></param>
        /// <returns></returns>
        [HttpPut, Description("修改商机")]
        [Authorize(AuthPolicyEnum.RequireRoleOfAdmin)]
        public async Task<bool> EditBusinessAsync(Guid keyValue, BusinessPool business) => await businessPoolBLL.SaveFormAsync(keyValue, business).ConfigureAwait(false);

        /// <summary>
        /// 删除商机
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpDelete, Description("删除商机")]
        public async Task<bool> DeleteBusinessAsync(Guid keyValue) => await businessPoolBLL.DeleteAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 商机转换客户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPut, Description("商机转换客户")]
        public async Task<bool> ConversionCustomerAsync(Guid keyValue) => await Task.Run(() => businessPoolBLL.ConversionCustomer(keyValue)).ConfigureAwait(false);
        #endregion

        #region 客户池
        /// <summary>
        /// 获取客户分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet, Description("获取客户列表")]
        public async Task<PagingResult<Customer>> GetCustomerPageAsync(Pagination pagination)
        {
            if (pagination is null)
            {
                pagination = new Pagination();
            }
            SqlSugar.RefAsync<int> totalNumber = default;
            var list = await customerBLL.GetPageListAsync(n => n.IsDelete == false && n.Status == StatusState.Normal && n.IsEnable == true, pagination, totalNumber).ConfigureAwait(false);
            pagination.Records = totalNumber.Value;
            return new PagingResult<Customer>(pagination) { Data = list };
        }

        /// <summary>
        /// 获取客户详情
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet, Description("读取客户详情")]
        public async Task<Customer> GetCustomerAsync(Guid keyValue) => await customerBLL.GetEntityAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost, Description("新增客户")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<bool> AddCustomerAsync(Customer customer) => await customerBLL.SaveFormAsync(default, customer).ConfigureAwait(false);

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut, Description("修改客户")]
        [Authorize(AuthPolicyEnum.RequireRoleOfAdmin)]
        public async Task<bool> EditCustomerAsync(Guid keyValue, Customer customer) => await customerBLL.SaveFormAsync(keyValue, customer).ConfigureAwait(false);

        /// <summary>
        /// 标记无效客户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPut, Description("标记无效客户")]
        public async Task InvalidCustomerAsync(Guid keyValue) => await Task.Run(() => customerBLL.Invalid(keyValue)).ConfigureAwait(false);

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpDelete, Description("删除客户")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<bool> DeleteCustomerAsync(Guid keyValue) => await customerBLL.DeleteAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 批量删除客户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete, Description("批量删除客户")]
        [Authorize(AuthPolicyEnum.RequireRoleOfSystemAdmin)]
        public async Task<bool> RangeDeleteCustomerAsync(List<string> ids) => customerBLL.RangeDelete(await customerBLL.GetListAsync(n => ids.Contains(n.ID.ToString())).ConfigureAwait(false));
        #endregion

        #region 跟进记录
        /// <summary>
        /// 获取商机<![CDATA[&]]>客户的跟进记录
        /// </summary>
        /// <param name="keyValue">商机ID</param>
        /// <returns></returns>
        [HttpGet, Description("读取跟进记录详情")]
        public async Task<List<CustomerTrailRecord>> GetTrailRecordAsync(string keyValue) => await trailRecordBLL.GetListAsync(n => n.BusinessPoolID == keyValue).ConfigureAwait(false);

        /// <summary>
        /// 新增跟进记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Description("新增跟进记录")]
        public async Task<bool> AddTrailRecord(CustomerTrailRecord entity) => await trailRecordBLL.SaveFormAsync(default, entity).ConfigureAwait(false);

        /// <summary>
        /// 修改跟进记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut, Description("修改跟进记录")]
        public async Task<bool> EditTrailRecord(Guid keyValue, CustomerTrailRecord entity) => await trailRecordBLL.SaveFormAsync(keyValue, entity).ConfigureAwait(false);

        /// <summary>
        /// 删除跟进记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpDelete, Description("删除跟进记录")]
        public async Task<bool> DeleteTrailRecord(Guid keyValue) => await trailRecordBLL.DeleteAsync(keyValue).ConfigureAwait(false);
        #endregion
    }
}