using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using C.O.S.E.C.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace C.O.S.E.C.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region  //�����֤ʱ��Ҫʹ�õķ���
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "COSEC";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                //options.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/shared-auth-ticket-keys/"));
                options.Cookie.Name = "COSEC";
                options.Cookie.Path = "/";
                options.LoginPath = new PathString("/user/login");
                options.AccessDeniedPath = new PathString("/Forbidden"); //��ֹ����·�������û���ͼ������Դʱ����δͨ������Դ���κ���Ȩ���ԣ����󽫱��ض���������·����
                options.SlidingExpiration = true;  //Cookie���Է�Ϊ�����Եĺ���ʱ�Եġ� ��ʱ�Ե���ָֻ�ڵ�ǰ�������������Ч�������һ���رվ�ʧЧ���������ɾ������ �����Ե���ָCookieָ����һ������ʱ�䣬�����ʱ�䵽��֮ǰ����cookieһֱ��Ч�������һֱ��¼�Ŵ�cookie�Ĵ��ڣ��� slidingExpriation�������ǣ�ָʾ�������cookie��Ϊ������cookie�洢�����ǻ��Զ����Ĺ���ʱ�䣬��ʹ�û������ڵ�¼��һֱ�������һ��ʱ���ȴ�Զ�ע����Ҳ����˵����10���¼�ˣ������������õ�TimeOutΪ30���ӣ����slidingExpriationΪfalse,��ô10:30�Ժ���ͱ������µ�¼�����Ϊtrue�Ļ�����10:16��ʱ����һ����ҳ�棬�������ͻ�֪ͨ��������ѹ���ʱ���޸�Ϊ10:46�� ����ϸ��˵�����ǲο�MSDN���ĵ���
            });
            #endregion
            services.AddControllersWithViews(configure =>
            {
                //configure.Filters.Add<LoginFilter>();
                //configure.RespectBrowserAcceptHeader = true;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
            }).AddSessionStateTempDataProvider();

            services.AddHttpClient("Api", opt =>
            {
                // opt.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "");
            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
