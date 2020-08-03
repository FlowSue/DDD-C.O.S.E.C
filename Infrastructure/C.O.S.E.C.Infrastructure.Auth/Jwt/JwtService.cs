//系统包
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//微软包
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//三方包
using Newtonsoft.Json;
//本地项目包
using C.O.S.E.C.Infrastructure.Auth.Enums;
using C.O.S.E.C.Infrastructure.Auth.Models;
using IdentityModel;

namespace C.O.S.E.C.Infrastructure.Auth.Jwt
{
    /// <summary>
    /// Jwt服务
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly JwtOption _jwtConfig;

        public JwtService(JwtSecurityTokenHandler jwtSecurityTokenHandler, JwtOption jwtConfig)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _jwtConfig = jwtConfig;
        }

        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public string IssueJwt(TokenModel tokenModel)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iss,"C.O.S.E.C"),//颁发人
                //new Claim(JwtRegisteredClaimNames.Aud,tokenModel.Uname+tokenModel.TokenType.ToString()+DateTime.Now.ToString()),//颁发对象
                new Claim(JwtRegisteredClaimNames.Sub,tokenModel.Uname.ToString()),//用户
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//用户Id
                new Claim(JwtClaimTypes.Name, tokenModel.Rname),//用户名
                new Claim(JwtClaimTypes.Role, tokenModel.Role),//身份
                //new Claim("proj", tokenModel.Project),//项目
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToUniversalTime().ToString(),ClaimValueTypes.Integer64),
                new Claim(ClaimTypeEnum.TokenModel.ToString(),JsonConvert.SerializeObject(tokenModel)),
            };
            var expMin = tokenModel.TokenType switch
            {
                TokenTypeEnum.Web => _jwtConfig.WebExp,
                TokenTypeEnum.App => _jwtConfig.AppExp,
                TokenTypeEnum.MiniProgram => _jwtConfig.MiniProgramExp,
                TokenTypeEnum.Other => _jwtConfig.OtherExp,
                _ => _jwtConfig.OtherExp,
            };
            DateTime expTime = dateTime.AddMinutes(expMin);

            Helper.CacheHelper.SetCacheValue($"Audience{tokenModel.TokenType}-{tokenModel.Uid}", tokenModel.Uname + tokenModel.TokenType.ToString() + DateTime.Now.ToString(), expMin);
            //秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                audience: tokenModel.TokenType.ToString(),
                issuer: "C.O.S.E.C",
                claims: claims,
                expires: expTime,//过期时间
                signingCredentials: creds);

            var encodedJwt = _jwtSecurityTokenHandler.WriteToken(jwt);

            return $"{JwtBearerDefaults.AuthenticationScheme} {encodedJwt}";
        }
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="uname"></param>
        /// <param name="role"></param>
        /// <param name="project"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public string IssueJwt(string uid, string uname = "Admin", string rname = "Admin", string role = "Admin", string systemId = default, string project = "C.O.S.E.C", TokenTypeEnum tokenType = TokenTypeEnum.Web)
        {
            return IssueJwt(new TokenModel() { Uid = uid, Uname = uname, Rname = rname, Role = role, Project = project, SystemId = systemId, TokenType = tokenType });
        }
        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public TokenModel SerializeJWT(string jwtStr)
        {
            var tm = new TokenModel();

            try
            {
                JwtSecurityToken jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtStr);
                jwtToken.Payload.TryGetValue("TokenModel", out object tokenModelObj);
                tm = JsonConvert.DeserializeObject<TokenModel>(tokenModelObj?.ToString());
            }
            catch (Exception ex)
            {
                // ignored
            }

            return tm;
        }
    }
}
