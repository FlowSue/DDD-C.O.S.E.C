using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.ViewModels;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using C.O.S.E.C.Infrastructure.Auth.Jwt;
using C.O.S.E.C.Infrastructure.CustomException;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.ComponentModel;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 登录系统接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController, AuthorizeFree]
    public class LoginController : ControllerBase
    {
        private readonly IUserInfoBLL usersBLL;

        private static Hashtable singleOnline = default;

        public static Hashtable SingleOnline { get => singleOnline; }

        public LoginController(IUserInfoBLL usersBLL)
        {
            this.usersBLL = usersBLL;
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="jwtService"></param>
        /// <returns></returns>
        [HttpPost, Description("登录")]
        public TokenEntity CheckLogin(LoginEntity entity, [FromServices] IJwtService jwtService)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (jwtService is null)
            {
                throw new ArgumentNullException(nameof(jwtService));
            }
            var data = usersBLL.CheckLogin(entity.Username, entity.Password);
            //var principal = HttpContext.User;
            //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, data.Account));
            //identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, data.Role.ToString()));
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, data.ID.ToString()));
            //identity.AddClaim(new Claim("SystemID", data.SystemID.ToString()));
            //principal.AddIdentity(identity);
            //await HttpContext.SignInAsync(identity.AuthenticationType, new ClaimsPrincipal(identity), new AuthenticationProperties
            //{
            //    AllowRefresh = true,
            //    IsPersistent = true,
            //    ExpiresUtc = new System.DateTimeOffset(dateTime: DateTime.Now.AddMinutes(5)),
            //});
            //HttpContext.Session.SetString("user", data.ToString());
            if (data.LoginOk && !data.ID.IsNullOrEmpty())
            {
                //FormsAuthentication.SetAuthCookie(data.UserName, false);
                data.Token = jwtService.IssueJwt(data.ID.ToString(), data.Account, data.NickName, data.Role.ToString(), data.SystemID, "CRM", (TokenTypeEnum)entity.OS);//? UserRoles.Client.ToString() : UserRoles.SystemAdmin.ToString());
                //Infrastructure.Helper.CacheHelper.SetCacheValue(data.Account, data.Token);
                GetOnline(data.Account, data.Token);
                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
                HttpContext.Response.Headers.Add("authorization", data.Token);
                HttpContext.Response.Cookies.Append("token", data.Token, new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    IsEssential = true
                });
                return new TokenEntity(data.Token);
            }
            else
            {
                throw new AppException(data.LoginMsg);
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Description("退出")]
        public void ExitLogin(string account)
        {
            var token = Request.Cookies?["token"];
            Response.Cookies.Delete("token");
            SingleOnline?.Remove(account);
            //HttpContext.SignOutAsync(account);
            //Infrastructure.Helper.CacheHelper.SetCacheValue($"Audience{tokenModel.TokenType.ToString()}-{tokenModel.Uid}", tokenModel.Uname + tokenModel.TokenType.ToString() + DateTime.Now.ToString(), -1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public void TakeToken(string tokenStr, [FromServices] IJwtService jwtService)
        {
            if (jwtService is null)
            {
                throw new ArgumentNullException(nameof(jwtService));
            }

            var tokenModel = jwtService.SerializeJWT(tokenStr);
            var token = jwtService.IssueJwt(tokenModel);
            Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
            Response.Headers.Add("authorization", token);
            Response.Cookies.Append("token", token);
        }

        private static void GetOnline(string Name, string token)
        {

            //Hashtable SingleOnline = default;// (Hashtable)HttpContext.Current.Application["Online"];
            if (SingleOnline == default)
                singleOnline = new Hashtable();

            if (SingleOnline.ContainsKey(Name))
            {
                SingleOnline[Name] = token;
            }
            else
                SingleOnline.Add(Name, token);

            //HttpContext.TraceIdentifier
            //System.Web.HttpContext.Current.Application.Lock();
            //System.Web.HttpContext.Current.Application["Online"] = SingleOnline;
            //System.Web.HttpContext.Current.Application.UnLock();
        }
#if false
        /// <summary>
        /// 检查应用接入的数据完整性
        /// </summary>
        /// <param name="signature">加密签名内容</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="appid">应用接入Id</param>
        /// <returns></returns>
        public CheckResult ValidateSignature(string signature, string timestamp, string nonce, string appid)
        {
            CheckResult result = new CheckResult();
            result.errmsg = "数据完整性检查不通过";

            //根据Appid获取接入渠道的详细信息
            AppInfo channelInfo = BLLFactory<App>.Instance.FindByAppId(appid);
            if (channelInfo != null)
            {
        #region 校验签名参数的来源是否正确
                string[] ArrTmp = { channelInfo.AppSecret, timestamp, nonce };

                Array.Sort(ArrTmp);
                string tmpStr = string.Join("", ArrTmp);

                tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
                tmpStr = tmpStr.ToLower();

                if (tmpStr == signature && ValidateUtil.IsNumber(timestamp))
                {
                    DateTime dtTime = timestamp.ToInt32().IntToDateTime();
                    double minutes = DateTime.Now.Subtract(dtTime).TotalMinutes;
                    if (minutes > timspanExpiredMinutes)
                    {
                        result.errmsg = "签名时间戳失效";
                    }
                    else
                    {
                        result.errmsg = "";
                        result.success = true;
                        result.channel = channelInfo.Channel;
                    }
                }
        #endregion
            }
            return result;
        }

    }

    internal class CheckResult
    {
        public string errmsg { get; internal set; }
        public bool success { get; internal set; }
        public object channel { get; internal set; }
#endif
    }
}