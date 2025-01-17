﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TCAssociationTool
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Default", // Route name
               "{controller}/{action}/{id}", // URL with parameters
               new { area = "User", controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
               new[] { "TCAssociationTool.Areas.User.Controllers" }
           ).DataTokens.Add("area", "User");
        }
    }
}