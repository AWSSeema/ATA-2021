using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Events,")]
    [Areas.Admin.Models.SessionClass.SessionExpireFilter]
    public class EventsController : Controller
    {
        TCAssociationTool.BLL.Events _Events = new TCAssociationTool.BLL.Events();
        TCAssociationTool.BLL.EventCategories _EventCategories = new TCAssociationTool.BLL.EventCategories(); 

        #region Events

        public ActionResult Index(string EventType = "")
        {               
            ViewBag.EventType = EventType;
            return View();
        }

        [HttpGet]
        public ActionResult EventsList(Int64 EventStatusId = 0, string EventType = "upcoming", string StartDate = "", string EndDate = "", string search = "", string SortColumn = "StartDate", string SortOrder = "ASC", int PageNo = 1, int PageItems = 10)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            List<Entities.Events> lstEventslist = new List<Entities.Events>();
            int Total = 0;
            try
            {
                lstEventslist = _Events.GetEventsListByVariable(EventStatusId, EventType, StartDate, EndDate, HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);
                
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.total = Total;
            ViewBag.items = PageItems;
            ViewBag.pageno = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.Eventslist = lstEventslist;
            return View();
        }

        public ActionResult AddEvent()
        {           
            List<Entities.EventCategories> lstEventCategories = new List<Entities.EventCategories>();

            int Total = 0;
            try
            {
                lstEventCategories = _EventCategories.GetEventCategoriesList(ref Total);
            }
            catch
            {
                Total = -1;
            }
            ViewBag.lstEventCategories = lstEventCategories;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddEvent(Entities.Events objEvents, List<Entities.EventRegistrationTypes> lstEventRegistrationTypes)
        {
            try
            {
               
                var image = WebImage.GetImageFromRequest();
                string imageurl = (image != null ? image.ImageFormat : "NA");

                objEvents.UpdatedBy = this.User.Identity.Name;
                if (lstEventRegistrationTypes != null && lstEventRegistrationTypes.Count != 0 && lstEventRegistrationTypes[0].RegistrationTitle != null)
                {
                    objEvents.XMLRegistrations = BLL.Common.CreateXMLForObject(lstEventRegistrationTypes);
                }
                Int64 _status = _Events.InsertEvent(objEvents, ref imageurl);
                switch (_status)
                {
                    case 1:
                        if (image != null)
                        {
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\banners\\" + imageurl);
                            var image_th = image;
                            image_th.Resize(150, 150);
                            image.Crop(1, 1, 1, 1);
                            image_th.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\banners\\thumb\\" + imageurl);
                        }
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        return RedirectToAction("Index", "Events");
                    case 2:
                        if (image != null)
                        {
                            image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\banners\\" + imageurl);
                            var image_th = image;
                            image_th.Resize(150, 150);
                            image.Crop(1, 1, 1, 1);
                            image_th.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\events\\banners\\thumb\\" + imageurl);
                        }
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        return RedirectToAction("EditEvent", "Events", new { EventId = objEvents.EventId });
                    case -1:
                        if (image != null)
                        {
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\events\\banners\\" + imageurl);
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\events\\banners\\thumb\\" + imageurl);
                        }
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed updating event details.</div>";
                        return RedirectToAction("Index", "Events");
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed updating event details.</div>";
                        return RedirectToAction("Index", "Events");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Events");
            }
        }

        [HttpPost]
        public JsonResult EventDelete(Int64 EventId, string ImageFileName)
        {
            string str = "";
            bool _bool = true;
            try
            {
                Int64 _status = _Events.DeleteEventById(EventId);
                switch (_status)
                {
                    case 1:
                        if (System.IO.File.Exists(ConfigurationManager.AppSettings["uploadPath"] + @"\events\banners\" + ImageFileName))
                        {
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\events\banners\" + ImageFileName);
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\events\banners\thumb\" + ImageFileName);
                        }
                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting event record</div>";
                        _bool = false;
                        break;
                    default:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting event record</div>";
                        _bool = false;
                        break;
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }
            return Json(new { ok = _bool, data = str });
        }

        public ActionResult EditEvent(Int64 EventId = 0,string  EventName="")
        {
           
            if (EventId == 0)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                return RedirectToAction("Index", "Events");
            }

            int status = 0;
            int Total = 0;
            List<Entities.EventCategories> lstEventCategories = new List<Entities.EventCategories>();
            List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>(); 
            Entities.Events objEvent = new Entities.Events();

            try
            {
                lstEventCategories = _EventCategories.GetEventCategoriesList(ref Total);
                objEvent = _Events.GetEventById(EventId, EventName,ref lstEventRegistrationTypes, ref status);
                if (objEvent == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Events");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Events");
            }

            ViewBag.objEvent = objEvent;

            ViewBag.lstEventCategories = lstEventCategories;
            ViewBag.lstEventRegistrationTypes = lstEventRegistrationTypes;
            ViewBag.status = (Total != -1 || status != -1 ? 1 : -1);

            return View();
        }

        public ActionResult ViewEvent(Int64 EventId = 0, string EventName="")
        {
           
            if (EventId == 0)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                return RedirectToAction("Index", "Events");
            }

            int status = 0;
            Entities.Events objEvent = new Entities.Events();
            List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>(); 
            try
            {
                objEvent = _Events.GetEventById(EventId,EventName,ref lstEventRegistrationTypes, ref status);
                if (objEvent == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Events");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Events");
            }

            ViewBag.objEvent = objEvent;
            ViewBag.lstEventRegistrationTypes = lstEventRegistrationTypes;
            ViewBag.status = status;

            return View();
        }

        [HttpPost]
        public JsonResult EventStatus(Int64 EventId)
        {
            string str = "";
            try
            {
                Int64 _status = _Events.UpdateEventStatus(EventId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Status Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = true, data = str });
            }
        }

        #endregion
         
        #region Event Controls

        public ActionResult EventMenu(Int64 EventId = 0, string EventName = "", string tab = "")
        {
            ViewBag.EventId = EventId;
            ViewBag.EventName = EventName;
            ViewBag.tab = tab;

            return View();
        }

        #endregion

        #region EventRegistrationTypes

          
        public ActionResult AddEventRegistrationTypes(Entities.EventRegistrationTypes objRegistrationTypes)
        {
            string str = "";
            bool _bool = true;
           
            Entities.EventRegistrationTypes objEventRegistrationTypes = new Entities.EventRegistrationTypes();
            try
            {
                objRegistrationTypes.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _Events.InsertRegistrationTypes(objRegistrationTypes);

               switch (_status)
                {
                    case 1:
                        str = "<div class=\"success closable\"> Inserted Registration Type details successfully.</div>";
                        _bool = true;
                        break;
                    case 2:
                        str = "<div class=\"success closable\"> Updated Registration Type details successfully.</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                    default:
                        str = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                }
            }
            catch 
            {
                str = "<div class=\"error closable\">Failed transaction.</div>";
                _bool = false;
            }

            return Json(new { ok = _bool, data = str });
        }  
        

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EventRegistrationTypesList(Int64 EventId = 0)
        {

            int Total = 0;
            List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();

            try
            {
                lstEventRegistrationTypes = _Events.GetEventRegistrationTypesList(EventId, ref Total);

            }
            catch
            {
                Total = -1;
            }

            ViewBag.lstEventRegistrationTypes = lstEventRegistrationTypes;
            return View();
        }


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeleteRegistrationType(Int64 EventRegistrationTypeId)
        {
            string str = "";
            try
            {

                Int64 _status = _Events.DeleteEventRegistrationTypes(EventRegistrationTypeId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult EventRegistrationTypesStatus(Int64 EventRegistrationTypeId)
        {
            string str = "";
            try
            {
                Int64 _status = _Events.UpdateEventRegistrationTypesStatus(EventRegistrationTypeId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Status Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        public JsonResult UpdateEventRegistrationTypesDisplayOrder(int OrderNo, Int64 EventRegistrationTypeId)
        {
            Int64 _status = 0;
            string str = "";
            _status = _Events.UpdateEventRegistrationTypesDisplayOrder(OrderNo, EventRegistrationTypeId);
            if (_status == 1)
            {
                str = "<div class=\"alert alert-success alert-dismissable\">Update Order Successfully</div>";
                return Json(new { ok = true, data = str });
            }
            else
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }

        }

        #endregion

    }
}
