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
    public class ScholarshipsController : Controller
    {
        BLL.Scholarships _Scholarships = new BLL.Scholarships();
        Entities.Scholarships objScholarships = new Entities.Scholarships();
        DAL.Scholarships _DScholarships = new DAL.Scholarships();
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult ScholarshipsList(string StartDate = "", string EndDate = "", string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.Scholarships> lstScholarships = new List<Entities.Scholarships>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstScholarships = _Scholarships.GetScholarshipsListByVariable(StartDate, EndDate, Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstScholarships = lstScholarships;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditScholarships(Int64 Id)
        {
            try
            {
                int status = 0;
                objScholarships = _Scholarships.GetScholarshipsById(Id, ref status);
                if (status != -1 && objScholarships != null)
                {
                    ViewBag.objScholarships = objScholarships;
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
        public ActionResult ViewScholarships(Int64 Id)
        {
            try
            {
                int status = 0;
                objScholarships = _Scholarships.GetScholarshipsById(Id, ref status);
                if (status != -1 && objScholarships != null)
                {
                    ViewBag.objScholarships = objScholarships;
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
        public ActionResult AddScholarships()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertScholarships(Entities.Scholarships objScholarships,HttpPostedFileBase DocumentUrl)
        {
            try
            {
                objScholarships.datesent = DateTime.UtcNow;
                var image = (DocumentUrl == null ? "NA" : Path.GetExtension(DocumentUrl.FileName));


                Int64 _status = _Scholarships.InsertScholarships(objScholarships, ref image);
                if (image != "")
                {
                    string FilePath = Path.Combine(ConfigurationManager.AppSettings["uploadPath"] + "\\documents\\", image);
                    DocumentUrl.SaveAs(FilePath);

                }
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
                    return RedirectToAction("Index", "Scholarships");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Scholarships");
        }



     


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Scholarships.DeleteScholarships(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Scholarships.</div>";
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
        public JsonResult ScholarshipsStatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Scholarships.UpdateScholarshipsStatus(id);
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



        public void ScholarshipsExporttoExcel(string Search = "", string StartDate = "", string EndDate = "", string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DScholarships.ExportScholarshipsList(Search, StartDate, EndDate, Sort);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Scholarships-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Scholarships-Registration-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
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
