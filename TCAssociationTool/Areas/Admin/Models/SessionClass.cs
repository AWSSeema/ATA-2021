using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace TCAssociationTool.Areas.Admin.Models
{
    public class SessionClass
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class SessionExpireFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;

                // If the browser session or authentication session has expired...
                if (ctx.Session["UserName"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // For AJAX requests, we're overriding the returned JSON result with a simple string,
                        // indicating to the calling JavaScript code that a redirect should be performed.
                        filterContext.Result = new JsonResult { Data = "_Logon_" };
                    }
                    else
                    {
                        // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                        // simply displays a temporary 5 second notification that they have timed out, and
                        // will, in turn, redirect to the logon page.
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            action = "LogOn",
                            controller = "Account",
                        }));
                    }
                }

                base.OnActionExecuting(filterContext);
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class LocsAuthorizeAttribute : AuthorizeAttribute
        {
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;

                // If the browser session has expired...
                if (ctx.Session["UserName"] == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // For AJAX requests, we're overriding the returned JSON result with a simple string,
                        // indicating to the calling JavaScript code that a redirect should be performed.
                        filterContext.Result = new JsonResult { Data = "_Logon_" };
                    }
                    else
                    {
                        // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                        // simply displays a temporary 5 second notification that they have timed out, and
                        // will, in turn, redirect to the logon page.
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary {
                            { "Controller", "Home" },
                            { "Action", "TimeoutRedirect" }
                    });
                    }
                }
                else if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    // Otherwise the reason we got here was because the user didn't have access rights to the
                    // operation, and a 403 should be returned.
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
                else
                {
                    base.HandleUnauthorizedRequest(filterContext);
                }
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class PermitAccessAttribute : AuthorizeAttribute
        {
            //public string Roles { get; set; }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                HttpContext ctx = HttpContext.Current;
                HttpCookie authCookie = HttpContext.Current.Request.Cookies["UserCookie"];
                if (authCookie == null || authCookie.Value == "")
                    return false;

                FormsAuthenticationTicket authTicket;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch
                {
                    return false;
                }

                List<string> userroles = authTicket.UserData.Split(',').ToList();

                foreach (var item in userroles)
                {
                    if (this.Roles.Contains(item) && item != null && item.Trim() != "")
                        return true;
                }

                return false;
            }
        }
    }
}