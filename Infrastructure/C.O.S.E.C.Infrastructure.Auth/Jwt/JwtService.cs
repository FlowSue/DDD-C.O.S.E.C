//系统包
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//微软包
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//三方包
using Newtonsoft.Json;
//本地项目包
using IdentityModel;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Domain.Enums.Auth;

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
                //new Claim(JwtRegisteredClaimNames.Aud,tokenModel.U_name+tokenModel.TokenType.ToString()+DateTime.Now.ToString()),//颁发对象
                new Claim(JwtRegisteredClaimNames.Sub,tokenModel.Uname),//用户
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid),//用户Id
                new Claim(JwtClaimTypes.Name, tokenModel.Rname),//用户名
                new Claim(JwtClaimTypes.Role, tokenModel.Role),//身份
                //new Claim("proj", tokenModel.Project),//项目
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToUniversalTime().ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64),
                new Claim(ClaimEnum.TokenModel.ToString(),JsonConvert.SerializeObject(tokenModel)),
            };
            var expMin = tokenModel.TokenType switch
            {
                TokenTypeEnum.Web => _jwtConfig.WebExp,
                TokenTypeEnum.App => _jwtConfig.AppExp,
                TokenTypeEnum.MiniProgram => _jwtConfig.MiniProgramExp,
                TokenTypeEnum.Other => _jwtConfig.OtherExp,
                _ => _jwtConfig.OtherExp,
            };
            var expTime = dateTime.AddMinutes(expMin);

            Helper.CacheHelper.SetCacheValue($"Audience{tokenModel.TokenType}-{tokenModel.Uid}", tokenModel.Uname + tokenModel.TokenType.ToString() + DateTime.Now.ToString(CultureInfo.InvariantCulture), expMin);
            //秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecurityKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                audience: tokenModel.TokenType.ToString(),
                issuer: "C.O.S.E.C",
                claims: claims,
                expires: expTime,//过期时间
                signingCredentials: cred);

            var encodedJwt = _jwtSecurityTokenHandler.WriteToken(jwt);

            return $"{JwtBearerDefaults.AuthenticationScheme} {encodedJwt}";
        }

        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="username">用户名</param>
        /// <param name="rename">昵称</param>
        /// <param name="role">身份</param>
        /// <param name="systemId">系统ID</param>
        /// <param name="project">项目</param>
        /// <param name="tokenType">token类型</param>
        /// <returns></returns>
        public string IssueJwt(string uid, string username = "Admin", string rename = "Admin", string role = "Admin", string systemId = default, string project = "C.O.S.E.C", TokenTypeEnum tokenType = TokenTypeEnum.Web)
        {
            return IssueJwt(new TokenModel() { Uid = uid, Uname = username, Rname = rename, Role = role, Project = project, SystemId = systemId, TokenType = tokenType });
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
                var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(jwtStr);
                jwtToken.Payload.TryGetValue("TokenModel", out var tokenModelObj);
                tm = JsonConvert.DeserializeObject<TokenModel>(tokenModelObj?.ToString()!);
            }
            catch (Exception)
            {
                // ignored
            }

            return tm;
        }
    }
}
