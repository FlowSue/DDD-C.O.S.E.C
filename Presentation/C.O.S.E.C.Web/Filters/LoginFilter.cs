using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C.O.S.E.C.Web.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect("/User/Login");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //HttpContext.Current.Response.Write("OnActionExecuted:Action执行时但还未返回结果时执行<br />");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // HttpContext.Current.Response.Write("OnResultExecuting:OnResultExecuting也和OnActionExecuted一样，但前者是在后者执行完后才执行<br />");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // HttpContext.Current.Response.Write("OnResultExecuted:是Action执行完后将要返回ActionResult的时候执行<br />");
        }
    }
}
