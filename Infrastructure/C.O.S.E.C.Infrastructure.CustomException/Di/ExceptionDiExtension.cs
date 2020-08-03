using C.O.S.E.C.Infrastructure.CustomException.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.CustomException.Di
{
    public static class ExceptionDiExtension
    {
        public static void UseExceptionService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();//自定义异常处理中间件
        }
    }
}
