﻿using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Auth.Jwt;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.Auth.Authorize
{
    /// <summary>
    /// 授权中间件
    /// </summary>
    public class JwtAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;

        public JwtAuthorizationMiddleware(RequestDelegate next,
            IJwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization'请求头，如果不包含则直接放行
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }

            string tokenHeader = httpContext.Request.Headers["Authorization"];
            tokenHeader = tokenHeader.Substring("Bearer ".Length).Trim();

            TokenModel tm = _jwtService.SerializeJWT(tokenHeader);

            //授权
            var claimList = new List<Claim>();
            var claim = new Claim(ClaimTypes.Role, tm.Role);
            claimList.Add(claim);
            var identity = new ClaimsIdentity(claimList);
            var principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            return _next(httpContext);
        }
    }
}
