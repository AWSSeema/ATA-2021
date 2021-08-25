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
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Enquiries,")]
    public class EnquiryController : Controller
    {
        TCAssociationTool.BLL.Enquiries _Enquiry = new TCAssociationTool.BLL.Enquiries();
        TCAssociationTool.Entities.Enquiries objRequestCalls = new TCAssociationTool.Entities.Enquiries();
        TCAssociationTool.DAL.Enquiries _DEnquiries = new TCAssociationTool.DAL.Enquiries();
        BLL.Events _Events = new BLL.Events();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
           int _qstatus = 0;
            ViewBag.eventlist = _Events.GetEnquiryEventsListall(ref _qstatus);
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EnquiriesList(Int64 EventId = 0, string StartDate = "", string EndDate = "", string search = "", string SortColumn = "", string SortOrder = "", int PageNo = 1, int PageItems = 10)
        {
            int Total = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            List<Entities.Enquiries> lstEnquiries = new List<Entities.Enquiries>();
            try
            {
                lstEnquiries = _Enquiry.GetEnquiriesListByVariable(EventId, StartDate, EndDate, HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);
               
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.items = PageItems;
            ViewBag.pageno = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            ViewBag.lstEnquiries =lstEnquiries;
            ViewBag.EventId = EventId;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult DeleteEnquiry(Int64 EnquiryId)
        {
            string str = "";
            try
            {
                Int64 _status = _Enquiry.DeleteEnquiry(EnquiryId);
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
        public ActionResult ViewEnquiry(Int64 EnquiryId)
        {
            Entities.Enquiries objEnquiriy = new Entities.Enquiries();
            int status = 0;
            try
            {
                objEnquiriy = _Enquiry.GetEnquirysById(EnquiryId, ref status);

                if (objEnquiriy == null)
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                    return RedirectToAction("Index", "Enquiry");
                }
                
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Sorry, failed processing your request.</div>";
                return RedirectToAction("Index", "Enquiry");
            }

            ViewBag.objEnquiriy = objEnquiriy;
            return View();
        }

        #region export to excel

        public void EnquiriesExporttoExcel(string Search = "", string SortColumn = "InsertedTime", string SortOrder = "DESC")
        {
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                DataTable dtUni = _DEnquiries.ExportToEnquiries(Search, Sort);
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
                    wb.Worksheets.Add(dtUni, "Enquiries-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Enquiries-Export-" + DateTime.UtcNow.ToString("dd-MM-yyyy") + ".xlsx");
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
