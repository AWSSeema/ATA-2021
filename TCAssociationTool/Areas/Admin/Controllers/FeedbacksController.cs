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
    public class FeedbacksController : Controller
    {
        BLL.Feedbacks _Feedbacks = new BLL.Feedbacks();
        Entities.Feedbacks objFeedbacks = new Entities.Feedbacks();
        DAL.Feedbacks _DFeedbacks = new DAL.Feedbacks();
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult FeedbacksList( string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.Feedbacks> lstFeedbacks = new List<Entities.Feedbacks>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstFeedbacks = _Feedbacks.GetFeedbacksListByVariable(Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstFeedbacks = lstFeedbacks;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
         
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditFeedbacks(Int64 Id)
        {
            try
            {
                int status = 0;
                objFeedbacks = _Feedbacks.GetFeedbacksById(Id, ref status);
                if (status != -1 && objFeedbacks != null)
                {
                    ViewBag.objFeedbacks = objFeedbacks;
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
        public ActionResult ViewFeedbacks(Int64 Id)
        {
            try
            {
                int status = 0;
                objFeedbacks = _Feedbacks.GetFeedbacksById(Id, ref status);
                if (status != -1 && objFeedbacks != null)
                {
                    ViewBag.objFeedbacks = objFeedbacks;
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
        public ActionResult AddFeedbacks()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertFeedbacks(Entities.Feedbacks objFeedbacks)
        {
            try
            {
                objFeedbacks.datesent = DateTime.UtcNow;

                Int64 _status = _Feedbacks.InsertFeedbacks(objFeedbacks);
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
                    return RedirectToAction("Index", "Feedbacks");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Feedbacks");
        }



        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FeedbacksCommentUpdate(Entities.Feedbacks objFeedbacks)
        {
            try
            {
                Int64 _status = _Feedbacks.FeedbacksCommentUpdate(objFeedbacks);
                if (_status != -1)
                {


                    if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Comments Successfully</div>";
                    }
                    return RedirectToAction("Index", "Feedbacks");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Feedbacks");
        }


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Feedbacks.DeleteFeedbacks(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Feedbacks.</div>";
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
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult FeedbacksDeleteAll(string Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Feedbacks.FeedbacksDeleteAll(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Feedbacks.</div>";
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



        public void FeedbacksExporttoExcel(string Search = "",  string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DFeedbacks.ExportFeedbacksList(Search, Sort);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Feedbacks-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Feedbacks-Registration-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
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
