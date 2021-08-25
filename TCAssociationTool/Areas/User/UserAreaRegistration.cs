using System.Web.Mvc;

namespace TCAssociationTool.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("index", "index.html", new { controller = "Home", action = "Index" });

            context.MapRoute("events", "{Type}-events.html", new { controller = "Event", action = "Index" });
            context.MapRoute("EventDetails", "event/{Type}/{EventName}.html", new { controller = "Event", action = "EventDetails" });

            context.MapRoute("CreateNewUser", "member-registration.html", new { controller = "Members", action = "AddMember" });
            context.MapRoute("memberAcknowledgement", "member-registration-acknowledgement.html", new { controller = "Members", action = "Acknowledgement" });
            context.MapRoute("member-acknowledgement", "member-acknowledgement.html", new { controller = "Members", action = "Thankyou" });
            context.MapRoute("ipn", "member/ipn.html", new { controller = "Members", action = "MemberIPN" });
            context.MapRoute("RenewalAcknowledgement", "member-renewal-acknowledgement.html", new { controller = "Members", action = "RenewalAcknowledgement" });
            context.MapRoute("members-list", "members-list.html", new { controller = "Members", action = "MemberList" });
            context.MapRoute("members-login", "members-login.html", new { controller = "Members", action = "LogOn" });


            context.MapRoute("contact-us", "contact-us.html", new { controller = "Home", action = "ContactUs" });

            context.MapRoute("donate-now", "donate-now.html", new { controller = "Donation", action = "Index" });
            context.MapRoute("donate-acknowledgement", "donation-acknowledgement.html", new { controller = "Donation", action = "Acknowledgement" });
            context.MapRoute("donars-list", "donars-list.html", new { controller = "Donation", action = "DonarsList" });


            context.MapRoute("leadership", "{CType}-leadership.html", new { controller = "Committee", action = "Index" });
            context.MapRoute("pastpresidents", "{CType}/{Year}-committee.html", new { controller = "Committee", action = "PastPresidents" });
            context.MapRoute("committee-categories-list", "committee-categories-list.html", new { controller = "Committee", action = "CommitteeCategoriesList" });

            context.MapRoute("login", "login.html", new { controller = "Members", action = "LogOn" });
            context.MapRoute("forgot-password", "forgot-password.html", new { controller = "Members", action = "ForgotPassword" });

            context.MapRoute("profile", "profile.html", new { controller = "Members", action = "Profile" });
            context.MapRoute("error-404", "error-404.html", new { controller = "Error", action = "Error404" });
            context.MapRoute("photo-gallery", "{Year}/photo-gallery.html", new { controller = "Gallery", action = "Photos" });
            context.MapRoute("photo-details", "{CategoryName}/gallery.html", new { controller = "Gallery", action = "PhotosList" });
            context.MapRoute("video-gallery", "video-gallery.html", new { controller = "Gallery", action = "Videos" });
            context.MapRoute("CulturalRegistration", "cultural-registration.html", new { controller = "Event", action = "CulturalRegistration" });
            context.MapRoute("EventRegistration", "event-registration.html", new { controller = "Event", action = "EventRegistration" });
            context.MapRoute("event-acknowledgement", "event-registration-acknowledgement.html", new { controller = "Event", action = "ThankYou" });
            context.MapRoute("volunteer", "volunteer-registration.html", new { controller = "Volunteer", action = "AddVolunteer" });
            context.MapRoute("thank-you", "thankyou.html", new { controller = "Volunteer", action = "ThankYou" });
            context.MapRoute("sponsors", "sponsors.html", new { controller = "Sponsors", action = "Index" });

            context.MapRoute("news", "news.html", new { controller = "News", action = "Index" });

            context.MapRoute("innerpage-details", "{PType}/{PageTitle}.html", new { controller = "InnerPages", action = "GetPageDetails" });
            context.MapRoute("innerpage-details1", "{PageTitle}.html", new { controller = "InnerPages", action = "GetPageDetails" });

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
