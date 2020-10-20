using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Auth.Attributes;
using C.O.S.E.C.Infrastructure.Auth.Jwt;
using C.O.S.E.C.Infrastructure.Config;
using C.O.S.E.C.Infrastructure.Cors.Attributes;
using C.O.S.E.C.Infrastructure.Cors.Enums;
using C.O.S.E.C.Infrastructure.CustomException;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace C.O.S.E.C.Api.Controllers
{
    /// <summary>
    /// 系统测试接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Cors(CorsPolicyEnum.Limit)]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AllConfigModel _allConfigModel;
        private readonly IWebHostEnvironment _env;
        private readonly IJwtService _jwtService;
        private readonly IEntityBaseAutoSetter _setter;

        public TestController(IConfiguration configuration,
            AllConfigModel allConfigModel,
            IWebHostEnvironment env,
            IJwtService jwtService,
            IEntityBaseAutoSetter setter)
        {
            _config = configuration;
            _allConfigModel = allConfigModel;
            _env = env;
            _jwtService = jwtService;
            _setter = setter;
        }

        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="uname"></param>
        /// <param name="role"></param>
        /// <param name="project"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        [Authorize(AuthPolicyEnum.Free)]
        public string GetJWTStr(string uid, string uname = "Admin", string role = "Admin", string project = "C.O.S.E.C", TokenTypeEnum tokenType = TokenTypeEnum.Web)
        {
            var tm = new TokenModel
            {
                Uid = uid,
                Uname = uname,
                Rname = uname,
                Role = role,
                Project = project,
                TokenType = tokenType
            };
            return _jwtService.IssueJwt(tm);
        }
        [HttpGet]
        [Route("SerializeJWT")]
        public TokenModel SerializeJWT(string jwtStr)
        {
            return _jwtService.SerializeJWT(jwtStr);
        }
        [HttpGet]
        [Route("time")]
        public DateTime Test(DateTime dateTime)
        {
            return dateTime;
        }

        /// <summary>
        /// 测试异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException", Name = "测试异常")]
        public IActionResult TestException()
        {
            string s = null;
            return Content(s.Length.ToString());
        }

        /// <summary>
        /// 测试异常2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException2")]
        public JsonResult TestException2()
        {
            var text = $"测试{DateTime.Now:d}";
            throw new System.Exception(text);
        }

        /// <summary>
        /// 测试异常3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException3")]
        public JsonResult TestException3()
        {
            throw new AppException();
        }

        /// <summary>
        /// 测试异常4
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException4")]
        public JsonResult TestException4()
        {
            throw ExceptionEx.ThrowBusinessException(new NotImplementedException());
        }
        /// <summary>
        /// 测试异常5
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException5")]
        public JsonResult TestException5()
        {
            throw ExceptionEx.ThrowComponentException(new NotImplementedException());
        }
        /// <summary>
        /// 测试异常6
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException6")]
        public JsonResult TestException6()
        {
            throw ExceptionEx.ThrowDataAccessException(new NotImplementedException());
        }
        /// <summary>
        /// 测试异常7
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException7")]
        public JsonResult TestException7()
        {
            throw ExceptionEx.ThrowServiceException(new NotImplementedException());
        }

        /// <summary>
        /// 测试配置1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestConfig1")]
        public string TestConfig1()
        {
            return _allConfigModel.TestConfigModel.Key1;
        }

        /// <summary>
        /// 测试配置2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestConfig2")]
        public string TestConfig2()
        {
            return _config["Test:Key1"];
        }

        [HttpGet]
        [Route("RootPath")]
        [Authorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
        public string RootPath(bool flag)
        {
            if (flag)
            {
                return _env.ContentRootPath;
            }
            else
            {
                return _env.WebRootPath;
            }
        }

        [HttpGet]
        [Route("CodeFirst")]
        public void CodeFirst()
        {

        }
    }
}
