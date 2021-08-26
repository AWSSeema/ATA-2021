using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class EventFormController : Controller
    {
        BLL.EventForm _EventForm = new BLL.EventForm();
        Entities.EventForm objEventForm = new Entities.EventForm();
        DAL.EventForm _DEventForm = new DAL.EventForm();
        List<Entities.Events> lstEvents  = new List<Entities.Events>();
        BLL.Events _Events = new BLL.Events();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            int status = 0;
            try
            {
                lstEvents = _Events.GetEventsList(ref status);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.lstEvents = lstEvents;
         
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EventFormList(Int64 eventid=0, string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.EventForm> lstEventForm = new List<Entities.EventForm>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstEventForm = _EventForm.GetEventFormListByVariable(eventid, Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstEventForm = lstEventForm;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditEventForm(Int64 Id)
        {
            try
            {
                int status = 0;
                objEventForm = _EventForm.GetEventFormById(Id, ref status);
                if (status != -1 && objEventForm != null)
                {
                    ViewBag.objEventForm = objEventForm;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewEventForm(Int64 Id)
        {
            try
            {
                int status = 0;
                objEventForm = _EventForm.GetEventFormById(Id, ref status);
                if (status != -1 && objEventForm != null)
                {
                    ViewBag.objEventForm = objEventForm;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult AddEventForm()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertEventForm(Entities.EventForm objEventForm)
        {
            try
            {
                objEventForm.datesent = DateTime.UtcNow;
                objEventForm.status2 = false;

                Int64 _status = _EventForm.InsertEventForm(objEventForm);
              
                if (_status != -1)
                {

                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                    }
                    else if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "EventForm");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "EventForm");
        }



        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EventFormCommentUpdate(Entities.EventForm objEventForm)
        {
            try
            {
                objEventForm.datesent = DateTime.UtcNow;
                objEventForm.status2 = false;

                Int64 _status = _EventForm.EventFormCommentUpdate(objEventForm);

                if (_status != -1)
                {

                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Comment has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "EventForm");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "EventForm");
        }




        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _EventForm.DeleteEventForm(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting EventForm.</div>";
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


        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult EventFormStatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _EventForm.UpdateEventFormStatus(id);
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



        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult EventFormDeleteAll(string Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _EventForm.EventFormDeleteAll(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting EventForm.</div>";
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
    }
}
