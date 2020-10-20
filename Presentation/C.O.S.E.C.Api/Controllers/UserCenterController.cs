using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.ViewModels;
using C.O.S.E.C.Infrastructure.CustomException;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 个人中心
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserCenterController : ControllerBase
    {
        private readonly IUserInfoBLL infoBLL;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="infoBLL"></param>
        public UserCenterController(IUserInfoBLL infoBLL)
        {
            this.infoBLL = infoBLL;
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet, Description("获取当前用户信息")]
        public async Task<UserInfo> GetInfoAsync(Guid keyValue) => await infoBLL.GetEntityAsync(keyValue).ConfigureAwait(false);

        /// <summary>
        /// 个性化
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Personalization()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        [HttpGet, Description("忘记密码")]
        public void ForgetPassword()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPut("/api/[controller]/{uid}/[action]"), Description("修改密码")]
        public void ChangePassword([FromRoute] string uid, PasswordModel password)
        {
            var user = infoBLL.GetEntity(Guid.Parse(uid));
            if (password.Password.Equals(user.Password, StringComparison.CurrentCulture))
            {
                infoBLL.RevisePasswordAsync(user.ID, password.NewPassword);
                Response.Cookies.Delete("token");
                //LoginController.SingleOnline?.Remove(user.Account);
                //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            else
            {
                throw new AppException("原密码不一致");
            }
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Description("修改个人信息")]
        public bool ChangeInfo()
        {
            throw new NotImplementedException();
        }


    }
}