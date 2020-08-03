using C.O.S.E.C.Infrastructure.Auth.Di;
using C.O.S.E.C.Infrastructure.Auth.Jwt;
using C.O.S.E.C.Infrastructure.Config;
using C.O.S.E.C.Infrastructure.Config.DI;
using C.O.S.E.C.Infrastructure.Config.FrameConfigModel;
using C.O.S.E.C.Infrastructure.Cors.Di;
using C.O.S.E.C.Infrastructure.CustomException.Di;
using C.O.S.E.C.Infrastructure.Swagger.DI;
using C.O.S.E.C.Infrastructure.Treasury.Di;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure
{
    public static class DiExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IWebHostEnvironment _env, IConfiguration Configuration)
        {
            services.AddMemoryCache();
            //注册配置管理服务
            services.AddConfigService(_env.ContentRootPath);
            AllConfigModel allConfig = services.GetSingletonInstanceOrNull<AllConfigModel>();

            //Swagger
            services.AddSwaggerService(Configuration);

            //注册授权认证
            JwtAuthConfigModel jwtConfig = allConfig.JwtAuthConfigModel;
            var jwtOption = new JwtOption//todo:使用AutoMapper替换
            {
                WebExp = jwtConfig.WebExp,
                AppExp = jwtConfig.AppExp,
                MiniProgramExp = jwtConfig.MiniProgramExp,
                OtherExp = jwtConfig.OtherExp,
                SecurityKey = jwtConfig.SecurityKey
            };
            services.AddAuthService(jwtOption);

            //注册Cors跨域
            services.AddCorsService();
            return services;
        }

        public static void UseInfrastructureService(this IApplicationBuilder app, IConfiguration Configuration)
        {
            app.UseSwaggerService(Configuration);

            app.UseExceptionService();
        }
        /// <summary>
        /// 需要放置在app.UseRouting()和app.UseEndpoints()之间
        /// </summary>
        /// <param name="app"></param>
        public static void UseInfrastructureAuthService(this IApplicationBuilder app)
        {
            app.UseCors();

            app.UseAuthService();

            app.UseAuthorization();
        }
    }
}
