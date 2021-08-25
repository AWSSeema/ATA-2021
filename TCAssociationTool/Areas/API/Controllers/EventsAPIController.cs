using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace TCAssociationTool.Areas.API.Controllers
{
    public class EventsAPIController : ApiController
    {
        BLL.Events _Events = new BLL.Events();



        [HttpGet]
        public HttpResponseMessage EventsList(string Type = "", string Search = "", string Sort = "", int PageNo = 1, int Items = 100)
        {
            int status = 0;
            int Total = 0;
            Entities.Events objEvents = new Entities.Events();
            List<Entities.Events> lstEvents = new List<Entities.Events>();

            try
            {
                string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/events/banners/";

                lstEvents = _Events.APIGetEventsList(Type, Search, Sort, PageNo, Items, ref Total);
                string msg = "success";

                var EventsDetailsList = lstEvents.Select(U => new { U.EventId, EventName = U.EventName, StartDate = Convert.ToDateTime(U.StartDate).ToString("dd MMM, yyyy hh:mm tt"), EndDate = Convert.ToDateTime(U.EndDate).ToString("dd MMM, yyyy hh:mm tt"), RegistrationStartDate = Convert.ToDateTime(U.RegistrationStartDate).ToString("dd MMM, yyyy hh:mm tt"), RegistrationEndDate = Convert.ToDateTime(U.RegistrationEndDate).ToString("dd MMM, yyyy hh:mm tt"), BannerUrl = (BLL.Common.ValidateImage(imgurl + U.BannerUrl, imgurl + "no-image.jpg")), U.EventCategoryId, U.MemberCount, Location = (U.Location != null && U.Location != "" ? U.Location : " "), City = (U.City != null && U.City != "" ? U.City : " "), StateName = (U.StateName != null && U.StateName != "" ? U.StateName : " "), ZipCode = (U.ZipCode != null && U.ZipCode != "" ? U.ZipCode : " "), EventDetails = (U.EventDetails != null && U.EventDetails != "" ? U.EventDetails : " "), U.IsRegistration, ContactEmail = (U.ContactEmail != null && U.ContactEmail != "" ? U.ContactEmail : " "), U.EventCategory, UpdatedTime = Convert.ToDateTime(U.UpdatedTime).ToString("dd MMM, yyyy") });
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, EventsDetailsList });
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetEventById(Int64 EventId = 0,string EventName="")
        {
            Entities.Events objEvents = new Entities.Events();
            Entities.EventRegistrationTypes objEventRegistrationTypes = new Entities.EventRegistrationTypes();

            int status = 0;
             List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();
      
             try
            {
                string imgurl = System.Configuration.ConfigurationManager.AppSettings["baseurl"] + "Content/events/banners/";
                objEvents = _Events.GetEventById(EventId, EventName, ref lstEventRegistrationTypes, ref status);
                var ImageUrl = (BLL.Common.ValidateImage(imgurl + objEvents.BannerUrl, imgurl + "no-image.jpg"));
                var StartDate = objEvents.StartDate.ToString("dd MMM, yyyy hh:mm tt");
                var EndDate = objEvents.EndDate.ToString("dd MMM, yyyy hh:mm tt");
                var RegistrationStartDate = objEvents.RegistrationStartDate.ToString("dd MMM, yyyy hh:mm tt");
                var RegistrationEndDate = objEvents.RegistrationEndDate.ToString("dd MMM, yyyy hh:mm tt");

                string msg = "success";
                
                var EventRegistrationTypesList = lstEventRegistrationTypes.Select(C => new { C.EventRegistrationTypeId,C.EventId,C.RegistrationTitle,C.RegCount,C.OrderNo,C.IsActive,C.MemberAmount,C.NonMemberAmount });

                if (objEvents.EventId != 0)
                {

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg, EventId = objEvents.EventId, BannerUrl = (BLL.Common.ValidateImage(imgurl + objEvents.BannerUrl, imgurl + "no-image.jpg")), City = (objEvents.City != null && objEvents.City != "" ? objEvents.City : " "), ContactEmail = objEvents.ContactEmail, EndDate = EndDate, EventCategoryId = objEvents.EventCategoryId, EventCategory = objEvents.EventCategory, EventDetails = (objEvents.EventDetails != null && objEvents.EventDetails != "" ? objEvents.EventDetails : " "), EventName = objEvents.EventName, IsRegistration = objEvents.IsRegistration, IsCulturalRegistration = objEvents.IsCulturalRegistration, Location = (objEvents.Location != null && objEvents.Location != "" ? objEvents.Location : " "), MemberCount = objEvents.MemberCount, RegistrationStartDate = RegistrationStartDate, RegistrationEndDate = RegistrationEndDate, StartDate = StartDate, StateName = (objEvents.StateName != null && objEvents.StateName != "" ? objEvents.StateName : " "), ZipCode = (objEvents.ZipCode != null && objEvents.ZipCode != "" ? objEvents.ZipCode : " "), EventRegistrationTypesList });
                }
                else
                {
                    msg = "No Event found for the given Id";
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { msg });
                }
            }
            catch
            {
                string msg = "failed";
                return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, msg);
            }
        }



    }
}
