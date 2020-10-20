using Autofac;
using C.O.S.E.C.Api.Hubs;
using C.O.S.E.C.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
        public IContainer Container { get; private set; }
        public IWebHostEnvironment env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructureService(env, Configuration);

            services.AddControllers(configure =>
            {
                configure.Filters.Add<Filters.WebApiResultFilterAttribute>();
                configure.RespectBrowserAcceptHeader = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
            //.AddNewtonsoftJson(opt =>
            //{
            //    opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //});


            //注册http上下文访问器
            services.AddHttpContextAccessor();

            // 添加Signalr
            services.AddSignalR(config =>
            {
                if (env.IsDevelopment())
                {
                    config.EnableDetailedErrors = true;
                }
            });
        }
        public static void ConfigureContainer(ContainerBuilder builder) => builder.RegisterModule(new Models.AutofacModule());
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
                endpoints.MapHub<ChatHub>("/notify-hub");
            });
        }
    }
}
