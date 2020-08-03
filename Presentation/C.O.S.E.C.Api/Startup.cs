using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C.O.S.E.C.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace C.O.S.E.C.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment _env)
        {
            Configuration = configuration;
            env = _env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment env { get; }
        //public IContainer Container { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(configure =>
            {
                configure.Filters.Add<Filters.WebApiResultFilterAttribute>();
                configure.RespectBrowserAcceptHeader = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
            //.AddNewtonsoftJson(opt =>
            //{
            //    opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //});

            services.AddInfrastructureService(env, Configuration);

            //注册http上下文访问器
            services.AddHttpContextAccessor();
            //services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            // 添加Signalr
            services.AddSignalR(config =>
            {
                if (env.IsDevelopment())
                {
                    config.EnableDetailedErrors = true;
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseInfrastructureService(Configuration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseInfrastructureAuthService();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
