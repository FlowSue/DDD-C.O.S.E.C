﻿using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Treasury.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.CustomException.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="env"></param>
        public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next;
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            bool isCatched = false;
            try
            {
                await _next(context);
            }
            //系统抛出的或自己throw的都会进到catch
            //进入到catch后，状态码为200，需要手动赋值
            catch (Exception ex)
            {
                if (ex is AppException rayAppException)//自定义业务异常
                {
                    context.Response.StatusCode = rayAppException.code;
                }
                else//系统异常
                {
                    context.Response.StatusCode = 500;
                    LogHelper.SetLog(LogLevel.Error, ex, _env.ContentRootPath);
                }
                await HandleExceptionAsync(context, context.Response.StatusCode, ex.Message);
                isCatched = true;
            }
            finally
            {
                if (!isCatched && context.Response.StatusCode != 200)//未捕捉过并且状态码不为200
                {
                    string msg = context.Response.StatusCode switch
                    {
                        401 => "未授权",
                        404 => "未找到服务",
                        403 => "访问被拒绝",
                        502 => "请求错误",
                        _ => "未知错误",
                    };
                    await HandleExceptionAsync(context, context.Response.StatusCode, msg);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var response = new ResponseObject { Code = (ResponseCode)statusCode, Info = msg };
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
