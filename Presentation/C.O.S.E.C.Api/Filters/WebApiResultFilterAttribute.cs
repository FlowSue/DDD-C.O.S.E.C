using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace C.O.S.E.C.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WebApiResultFilterAttribute : ActionFilterAttribute
    {
        public WebApiResultFilterAttribute()
        {
        }

        //private readonly _ISystemActionLogBLL log;

        //public WebApiResultFilterAttribute(_ISystemActionLogBLL log)
        //{
        //    this.log = log;
        //}

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.OnActionExecuted(context);
            if (!context.Result.IsEmpty())
            {
                if (!context.ActionDescriptor.RouteValues["controller"].Contains("Test", StringComparison.CurrentCulture))
                {
                    var info = (context.HttpContext.RequestServices.GetService(typeof(IOperateInfo)) as IOperateInfo).TokenModel;
                    //_ = log.SaveFormAsync(default, new _SystemActionLog()
                    //{
                    //    ActionPath = context.HttpContext.Request.Path.Value,
                    //    RequestIP = $"{context.HttpContext.Connection.RemoteIpAddress.MapToIPv4()}:{context.HttpContext.Connection.RemotePort}",
                    //    ActionName = context.ActionDescriptor.RouteValues["action"],
                    //    ControllerName = context.ActionDescriptor.RouteValues["controller"],
                    //    Description = context.ActionDescriptor.AttributeRouteInfo.Name ?? "",//Action--Route--Name
                    //    CreateUserID = info.Uid ?? "",
                    //    CreateUserName = info.Uname ?? "",
                    //    UpdateUserID = info.Uid ?? "",
                    //    UpdateUserName = info.Uname ?? "",
                    //    SystemID = info.SystemId ?? ""
                    //});
                }
            }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        /// <summary>
        /// 包装接口返回类型
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (!context.ActionDescriptor.RouteValues["controller"].Contains("Internal", StringComparison.CurrentCulture))
            {
                switch (context.Result)
                {
                    //根据实际需求进行具体实现
                    case ForbidResult _:
                    case NotFoundResult _:
                    case NotFoundObjectResult _:
                    case BadRequestResult _:
                    case BadRequestObjectResult _:
                    case OkObjectResult _:
                        break;
                    case ObjectResult objectResult:
                        context.Result = objectResult.Value.IsEmpty()
                            ? new ObjectResult(new ResponseObject { Code = (ResponseCode)404, Info = "未找到资源" })
                            : new ObjectResult(new ResponseObject { Code = (ResponseCode)200, Info = "响应成功", Data = objectResult.Value });
                        break;
                    case EmptyResult _:
                        //context.Result = new ObjectResult(new ResponseParameter { code = (ResponseCode)404, info = "未找到资源" });
                        break;
                    case ContentResult contentResult:
                        context.Result = new ObjectResult(new ResponseObject { Code = (ResponseCode)200, Info = contentResult.Content, Data = contentResult.Content });
                        break;
                    case StatusCodeResult statusResult:
                        context.Result = new ObjectResult(new ResponseObject { Code = (ResponseCode)statusResult.StatusCode, Info = string.Empty });
                        break;
                    default:
                        break;
                }
            }
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }
    }
}
