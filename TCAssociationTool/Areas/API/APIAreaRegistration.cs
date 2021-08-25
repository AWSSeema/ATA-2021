using System.Web.Mvc;
using System.Web.Http;

namespace TCAssociationTool.Areas.API
{
    public class APIAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "API";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapHttpRoute(
                    name: "TCAssociationToolApiAction",
                    routeTemplate: "api/{controller}/{action}"
                );

            context.Routes.MapHttpRoute(
                    name: "TCAssociationToolApiAction_1",
                    routeTemplate: "api/{controller}"
                );

            context.Routes.MapHttpRoute(
                name: "ControllersApi",
                 routeTemplate: "api/{controller}/{action}/{id}",
                  defaults: new { id = RouteParameter.Optional }
            );

            context.MapRoute("donate-now-api", "api-donate", new { controller = "DonateNow", action = "Index" });
            context.MapRoute("member-api", "api-member", new { controller = "Member", action = "AddMember" });
            context.MapRoute("event-registration-api", "api-event", new { controller = "Event", action = "EventRegistration" });

            context.MapRoute(
                "AdminPanel_dashboard",
                "API/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "API_default",
                "API/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
