using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Volunteers")]
    public class VolunteersController : Controller
    {
        BLL.Volunteers _Volunteers = new BLL.Volunteers();
        BLL.VolunteerCategories _VolunteerCategory = new BLL.VolunteerCategories();
        List<Entities.VolunteerCategories> lstVolunteerCategory = new List<Entities.VolunteerCategories>();
        BLL.Events _Events = new BLL.Events();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();
        DAL.Volunteers _DVolunteers = new DAL.Volunteers();

        #region Volunteers

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            int _qstatus = 0;
            ViewBag.catlist = _VolunteerCategory.GetVolunteerCategoriesList(ref _qstatus);
            ViewBag.eventlist = _Events.GetEventsListall(ref _qstatus);
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult VolunteersList(Int64 VolunteerCategoryId = 0, Int64 EventId = 0, string Search = "", string SortColumn = "UpdatedDate", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            List<Entities.Volunteers> lstVolunteers = new List<Entities.Volunteers>();

            try
            {
                lstVolunteers = _Volunteers.GetVolunteersListByVariable(VolunteerCategoryId, EventId, Search, Sort, PageNo, Items, ref Total);

            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstVolunteers = lstVolunteers;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddVolunteer(Int64 VolunteerCategoryId = 0, Int64 EventId = 0)
        {
            int _qstatus = 0;
            int status = 0;
             try
            {
            List<Entities.VolunteerCategories> lstVolunteerCategory = _VolunteerCategory.GetVolunteerCategoriesList(ref _qstatus);
          
            List<Entities.Events> lstEvents = _Events.GetEventsListall(ref status);
            if (_qstatus == 1 || status == 1)
            {
                ViewBag.lstVolunteerCategory = lstVolunteerCategory;
                ViewBag.lstEvents = lstEvents;
                ViewBag.status = _qstatus;
            }
            else
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "VideoGallery");
            }
            }
             catch
             {
                 TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
             }
             ViewBag.VolunteerCategoryId = VolunteerCategoryId;
             ViewBag.EventId = EventId;
             return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddVolunteer(Entities.Volunteers objVolunteers)
        {
            try
            {
                objVolunteers.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objVolunteers.InsertedBy = HttpContext.User.Identity.Name.ToString();
                objVolunteers.IsActive = false;
                Int64 _status = _Volunteers.InsertVolunteers(objVolunteers);
                if (_status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Index", "Volunteers");
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("EditVolunteer", "Volunteers", new { VolunteerId = objVolunteers.VolunteerId });
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("AddVolunteer", "Volunteers");
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditVolunteer(Int64 VolunteerId = 0, Int64 VolunteerCategoryId = 0)
        {
            try
            {

                int _qstatus = 0;
                Entities.Volunteers _objVolunteers = _Volunteers.GetVolunteerInfoById(VolunteerId, ref _qstatus);
                int qstatus = 0;
                List<Entities.VolunteerCategories> lstVolunteerCategory = _VolunteerCategory.GetVolunteerCategoriesList(ref qstatus);
                int status = 0;
                List<Entities.Events> lstEvents = _Events.GetEventsListall(ref status);

                if (_qstatus == 1 && (qstatus == 1 || status == 1))
                {
                    ViewBag.objVolunteers = _objVolunteers;
                    ViewBag.VolunteerCategoryId = VolunteerCategoryId;
                    ViewBag.lstVolunteerCategory = lstVolunteerCategory;
                    ViewBag.lstEvents = lstEvents;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Volunteers");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Volunteers");
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewVolunteer(Int64 VolunteerId = 0)
        {
             int _qstatus = 0;
             Entities.Volunteers _objVolunteers = new Entities.Volunteers();
            try
            {
                _objVolunteers = _Volunteers.GetVolunteerInfoById(VolunteerId, ref _qstatus);

                if (_qstatus != 1)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Volunteers");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Volunteers");
            }

            ViewBag.objVolunteers = _objVolunteers;
            ViewBag.status = _qstatus;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeleteVolunteer(Int64 VolunteerId)
        {
            string str = "";
            try
            {

                Int64 _status = _Volunteers.DeleteVolunteer(VolunteerId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting page</div>";
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
        public JsonResult Volunteerstatus(Int64 VolunteerId)
        {
            string str = "";
            int status = 0;
            try
            {
                Int64 _status = _Volunteers.UpdateVolunteerstatus(VolunteerId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Status Successfully</div>";
                    int status1 = 0;
                    Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);
                    Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("Confirm Volunteer Registration", 0, ref status);
                    if (objTemplates != null)
                    {
                        Entities.Volunteers objVolunteers = _Volunteers.GetVolunteerInfoById(VolunteerId, ref status);
                        StringBuilder body = new StringBuilder();
                        if (objVolunteers.IsActive == true)
                        {
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["usersiteurl"].ToString());
                            body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body.Replace("[SiteName]", objAppInfo.SiteName);
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objVolunteers.FirstName + "" + objVolunteers.LastName));
                            //body.Replace("[EVENTNAME]", BLL.Common.UppercaseFirst(objVolunteers.VolunteerCategoryId));
                            BLL.Common.SendMail(objVolunteers.Email, objTemplates.Subject, body.ToString());
                        }
                    }
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
        public JsonResult CheckVolunteerEmailAvailability(string Email)
        {
            int status = 0;
            try
            {
                Entities.Volunteers objVolunteers = _Volunteers.VolunteerValidationByEmail(Email, ref status);
                bool data = (objVolunteers.VolunteerId != 0 ? false : true);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>" });
            }
        }


        [HttpPost]
        public JsonResult CheckEmailAvailability(string Email,Int64 VolunteerId)
        {
            int status = 0;
            try
            {
                Entities.Volunteers objVolunteers = _Volunteers.VolunteerValidationByEmail(Email, ref status);
                bool data = (objVolunteers.VolunteerId == VolunteerId || objVolunteers.VolunteerId == 0 ? true : false);

                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>" });
            }
        }

        #endregion      

        #region export to excel

        public void VolunteersExporttoExcel(string Search = "", Int64 VolunteerCategoryId = 0, Int64 EventId = 0, string SortColumn = "UpdatedDate", string SortOrder = "DESC")
        {
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                DataTable dtUni = _DVolunteers.ExportToVolunteers(Search, VolunteerCategoryId, EventId, Sort);
                //MemoryStream stream = BLL.Common.CSVUtility.GetCSV(dtUni);

                //var filename = "XLSX-Enquiries-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".xlsx";
                //var contenttype = "text/csv";
                //Response.Clear();
                //Response.ContentType = contenttype;
                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(stream.ToArray());
                //Response.End();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Volunteers-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Volunteers-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
        }

        #endregion
    }
}
