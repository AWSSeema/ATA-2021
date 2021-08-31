using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class CulturalController : Controller
    {
        TCAssociationTool.BLL.Cultural _Cultural = new TCAssociationTool.BLL.Cultural();
        Entities.Members objMembers = new Entities.Members();
        BLL.Members _Members = new BLL.Members();
        DAL.Cultural _DCultural = new DAL.Cultural();
        BLL.CulturalLocations _CulturalLocations = new BLL.CulturalLocations();
        List<Entities.CulturalLocations> lstCulturalLocations = new List<Entities.CulturalLocations>();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            try
            {
                int _qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref _qstatus);
                lstCulturalLocations = _CulturalLocations.GetCulturalLocationslist(ref _qstatus);


                if (_qstatus == 1)
                {
                    ViewBag.objMembers = objMembers;
                    ViewBag.lstCulturalLocations = lstCulturalLocations;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Members");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult CulturalList(string StartDate="", string EndDate="", Int64 PaymentStatus =0, string category="", string location ="",string search = "", string SortColumn = "datesent", string SortOrder = "Desc", int PageNo = 1, int PageItems = 10)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            List<Entities.Cultural> lstCultural = new List<Entities.Cultural>();
            try
            {
                lstCultural = _Cultural.GetCulturalListByVariable(StartDate, EndDate,PaymentStatus, category, location, HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.Total = Total;
            ViewBag.items = PageItems;
            ViewBag.PageNo = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.lstCultural = lstCultural;
            return View();
        }

        public JsonResult DeleteCultural(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Cultural.DeleteCultural(id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting item</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewCultural(Int64 id)
        {
            Entities.Cultural objCultural = new Entities.Cultural();
            int status = 0;
            try
            {
                int qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref qstatus);

                if (qstatus == 1)
                {
                    ViewBag.objMembers = objMembers;
                    ViewBag.status = qstatus;
                }

                objCultural = _Cultural.GetCulturalById(id, ref status);

                if (objCultural == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Cultural");
                }

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "Cultural");
            }

            ViewBag.objCultural = objCultural;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCultural(Int64 id)
        {
            Entities.Cultural objCultural = new Entities.Cultural();
          
            int status = 0;
           
            try
            {
                int qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref qstatus);

                if (qstatus == 1)
                {
                    ViewBag.objMembers = objMembers;
                    ViewBag.status = qstatus;
                }

                objCultural = _Cultural.GetCulturalById(id, ref status);
                if (objCultural == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Cultural");
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                return RedirectToAction("Index", "Cultural");
            }
          
            ViewBag.objCultural = objCultural;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddCultural(Entities.Cultural objCultural)
        {
            try
            {
                Int64 id = 0;

                //objCultural.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                //objCultural.InsertedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _Cultural.InsertCultural(objCultural, ref id);
                if (_status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Index", "Cultural");
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("ViewCultural", "Cultural",new { id=id});
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading page.</div>";
                    return RedirectToAction("Index", "Cultural");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "Cultural");

            }

            return View();
        }

        [Models.SessionClass.SessionExpireFilter]

        public ActionResult AddCultural(Int64 id = 0)
        {
           return View();
        }

        public void CulturalExporttoExcel(string StartDate = "", string EndDate = "", Int64 PaymentStatus = 0, string category = "", string location = "", string Search = "", string SortColumn = "datesent", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DCultural.ExportCulturalList(StartDate, EndDate, PaymentStatus, category, location, Search,Sort);
                //MemoryStream stream = BLL.Common.CSVUtility.GetCSV(dtUni);

                //var filename = "CSV-Members-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".csv";
                //var contenttype = "text/csv";
                //Response.Clear();
                //Response.ContentType = contenttype;
                //Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(stream.ToArray());
                //Response.End();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Cultural-Reg-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Cultural-Reg-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
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
        [HttpPost]
        public JsonResult CulturalsDeleteAll(string id)
        {
            string str = "";
            try
            {
                Int64 _status = _Cultural.DeleteAllCulturals(id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Records Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"error-alert closable\">Failed deleting members.</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }
    }
}
