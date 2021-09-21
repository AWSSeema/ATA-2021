using TCAssociationTool.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ClosedXML.Excel;
namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class CulturalRegistrationsController : Controller
    {
        BLL.CulturalRegistrations _CulturalRegistrations = new BLL.CulturalRegistrations();
        Entities.CulturalRegistrations objCulturalRegistrations = new Entities.CulturalRegistrations();
        DAL.CulturalRegistrations _DCulturalRegistrations = new DAL.CulturalRegistrations();
       
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
        

            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult CulturalRegistrationsList(string ItemType="", string StartDate="", string EndDate="", string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.CulturalRegistrations> lstCulturalRegistrations = new List<Entities.CulturalRegistrations>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstCulturalRegistrations = _CulturalRegistrations.GetCulturalRegistrationsListByVariable(ItemType,StartDate, EndDate, Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstCulturalRegistrations = lstCulturalRegistrations;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCulturalRegistrations(Int64 Id)
        {
            try
            {
                int status = 0;
                objCulturalRegistrations = _CulturalRegistrations.GetCulturalRegistrationsById(Id, ref status);
                if (status != -1 && objCulturalRegistrations != null)
                {
                    ViewBag.objCulturalRegistrations = objCulturalRegistrations;
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
        public ActionResult ViewCulturalRegistrations(Int64 Id)
        {
            try
            {
                int status = 0;
                objCulturalRegistrations = _CulturalRegistrations.GetCulturalRegistrationsById(Id, ref status);
                if (status != -1 && objCulturalRegistrations != null)
                {
                    ViewBag.objCulturalRegistrations = objCulturalRegistrations;
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
        public ActionResult AddCulturalRegistrations()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertCulturalRegistrations(Entities.CulturalRegistrations objCulturalRegistrations)
        {
            try
            {
                objCulturalRegistrations.date_created = DateTime.UtcNow;
                objCulturalRegistrations.date_modified = DateTime.UtcNow;
                objCulturalRegistrations.status2 = false;

                Int64 _status = _CulturalRegistrations.InsertCulturalRegistrations(objCulturalRegistrations);

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
                    return RedirectToAction("Index", "CulturalRegistrations");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "CulturalRegistrations");
        }



        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CulturalRegistrationsCommentUpdate(Entities.CulturalRegistrations objCulturalRegistrations)
        {
            try
            {
               
                Int64 _status = _CulturalRegistrations.CulturalRegistrationsCommentUpdate(objCulturalRegistrations);

                if (_status != -1)
                {

                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Comment has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "CulturalRegistrations");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "CulturalRegistrations");
        }




        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _CulturalRegistrations.DeleteCulturalRegistrations(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting CulturalRegistrations.</div>";
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
        public JsonResult CulturalRegistrationsStatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _CulturalRegistrations.UpdateCulturalRegistrationsStatus(id);
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
        public JsonResult CulturalRegistrationsDeleteAll(string Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _CulturalRegistrations.CulturalRegistrationsDeleteAll(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting CulturalRegistrations.</div>";
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

        public void CulturalRegistrationsExporttoExcel(string Search = "", string ItemType="",string StartDate="", string EndDate="", string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DCulturalRegistrations.ExportCulturalRegistrationsList(Search, ItemType, StartDate, EndDate, Sort);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Cultural-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Cultural-Registration-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
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

    }
}
