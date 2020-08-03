using C.O.S.E.C.Infrastructure.Cors.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Cors.Di
{
    public static class CorsDiExtension
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });

                c.AddPolicy(CorsPolicyEnum.Free.ToString(), policy =>
                {
                    policy.WithOrigins("http://*.ijunao.com", "https://*.ijunao.com", "https://*.flowsue.top")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });

                c.AddPolicy(CorsPolicyEnum.Limit.ToString(), policy =>
                {
                    policy.WithOrigins("http://*.ijunao.com", "https://*.ijunao.com", "https://*.flowsue.top")
                    .WithMethods("get", "post", "put", "delete")
                    //.WithHeaders("Authorization");
                    .AllowAnyHeader();
                }); ;
            });
            return services;
        }
    }
}
