using C.O.S.E.C.Domain.Enums.Auth;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Infrastructure.Auth.Authorize;
using C.O.S.E.C.Infrastructure.Auth.Jwt;
using C.O.S.E.C.Infrastructure.Auth.Operate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.Auth.Di
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class AuthDiExtension
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services, JwtOption jwtOption)
        {
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddSingleton(jwtOption);
            services.AddSingleton<IJwtService, JwtService>();
            //services.AddAuthentication()
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //    {
            //        o.LoginPath = new PathString("/Login/Index");
            //        o.AccessDeniedPath = new PathString("/Error/Forbidden");
            //    });
            #region 注册【认证】服务
            services.AddAuthentication(x =>
            {
                //x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //x.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //x.DefaultChallengeScheme = OAuthDefaults.DisplayName;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {

                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                    ValidIssuer = "C.O.S.E.C",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOption.SecurityKey)),
                    //AudienceValidator = (m, n, z) =>
                    //{
                    //    return m != null && m.FirstOrDefault().Equals(Helper.CacheHelper.GetCacheValue($"Audience{m.LastOrDefault()}-{n.Id}")?.ToString());
                    //},
                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    //RequireSignedTokens = true,
                    //RequireExpirationTime = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    ValidateAudience = false,
                    //ValidateIssuer = true,
                    //ValidateIssuerSigningKey = true,
                    //ClockSkew = TimeSpan.FromSeconds(5),// 允许的服务器时间偏移量
                    //ValidateLifetime = true// 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            })
            //.AddOAuth(OAuthDefaults.DisplayName, options =>
            //{
            //    options.ClientId = "oauth.code";
            //    options.ClientSecret = "secret";
            //    options.AuthorizationEndpoint = "https://oidc.faasx.com/connect/authorize";
            //    options.TokenEndpoint = "https://oidc.faasx.com/connect/token";
            //    options.CallbackPath = "/signin-oauth";
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("email");
            //    options.SaveTokens = true;
            //    // 事件执行顺序 ：
            //    // 1.创建Ticket之前触发
            //    options.Events.OnCreatingTicket = context => Task.CompletedTask;
            //    // 2.创建Ticket失败时触发
            //    options.Events.OnRemoteFailure = context => Task.CompletedTask;
            //    // 3.Ticket接收完成之后触发
            //    options.Events.OnTicketReceived = context => Task.CompletedTask;
            //    // 4.Challenge时触发，默认跳转到OAuth服务器
            //    // options.Events.OnRedirectToAuthorizationEndpoint = context => context.Response.Redirect(context.RedirectUri);
            //})
            ;
            #endregion

            #region 注册【授权】服务
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthPolicyEnum.Free.ToString(), policy => policy.AddRequirements(new PolicyRequirement(false)));
                options.AddPolicy(AuthPolicyEnum.RequireRoleOfClient.ToString(), policy => policy.AddRequirements(new PolicyRequirement("SystemAdmin,Client")));
                options.AddPolicy(AuthPolicyEnum.RequireRoleOfAdmin.ToString(), policy => policy.AddRequirements(new PolicyRequirement("SystemAdmin,Admin")));
                options.AddPolicy(AuthPolicyEnum.RequireRoleOfAdminOrClient.ToString(), policy => policy.AddRequirements(new PolicyRequirement("SystemAdmin,Admin,Client")));
                options.AddPolicy(AuthPolicyEnum.RequireRoleOfSystemAdmin.ToString(), policy => policy.AddRequirements(new PolicyRequirement("SystemAdmin")));
            });
            #endregion

            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();

            //注册IOperateInfo
            services.AddScoped<IOperateInfo, OperateInfo>();
            services.AddScoped<IEntityBaseAutoSetter, OperateSetter>();

            return services;
        }

        public static void UseAuthService(this IApplicationBuilder app)
        {
            //认证
            app.UseAuthentication();

            //授权
            //app.UseMiddleware<JwtAuthorizationMiddleware>();
        }
    }
}