using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,NewsLetter")]
    public class NewsLetterController : Controller
    {
        TCAssociationTool.BLL.NewsLetter _NewsLetter = new TCAssociationTool.BLL.NewsLetter();
        TCAssociationTool.DAL.NewsLetter _DNewsLetter = new TCAssociationTool.DAL.NewsLetter();
        TCAssociationTool.Entities.NewsLetter objNewsLetter = new TCAssociationTool.Entities.NewsLetter();
      
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult NewsLetterList(string search = "",string SortColumn = "", string SortOrder = "", int PageNo = 1, int PageItems = 10)
        {
            int Total = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            List<Entities.NewsLetter> lstNewsLetter = new List<Entities.NewsLetter>();
            try
            {
                lstNewsLetter = _NewsLetter.GetNewsLetterListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);               
            }
            catch 
            {
                Total = -1;
            }

            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.Total = Total;
            ViewBag.items = PageItems;
            ViewBag.PageNo = PageNo;
            ViewBag.NewsLetterlist = lstNewsLetter;
            return View();
        }


       [HttpPost]
       [Areas.Admin.Models.SessionClass.SessionExpireFilter]

        public JsonResult DeleteNewsLetter(Int64 LetterId)
        {
            string str = "";
            try
            {
                Int64 _status = _NewsLetter.DeleteNewsLetter(LetterId);
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
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult NewsLetterStatus(Int64 LetterId)
        {
            string str = "";
            try
            {
                Int64 _status = _NewsLetter.UpdateNewsLettertatusById(LetterId);
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
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }
        }

        //public void NewsLetterExporttoExcel(string Search = "", string SortColumn = "RegisteredDate", string SortOrder = "DESC")
        //{
        //    string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
        //    try
        //    {
        //        DataTable dtUni = _DNewsLetter.ExportNewsLetterGetList(Search,Sort);
        //        MemoryStream stream = BLL.Common.CSVUtility.GetCSV(dtUni);

        //        var filename = "CSV-Subscribers-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".csv";
        //        var contenttype = "text/csv";
        //        Response.Clear();
        //        Response.ContentType = contenttype;
        //        Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.BinaryWrite(stream.ToArray());
        //        Response.End();
        //    }
        //    catch
        //    {
        //        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
        //    }
        //}

        public void NewsLetterExporttoExcel(string Search = "", string SortColumn = "RegisteredDate", string SortOrder = "DESC")
        {
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                DataTable dtUni = _DNewsLetter.ExportNewsLetterGetList(Search, Sort);
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
                    wb.Worksheets.Add(dtUni, "NewsLetter-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=NewsLetter-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".xlsx");
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
