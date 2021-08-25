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
    public class Golf2021Controller : Controller
    {
        BLL.Golf2021 _Golf2021 = new BLL.Golf2021();
        Entities.Golf2021 objGolf2021 = new Entities.Golf2021();
        DAL.Golf2021 _DGolf2021 = new DAL.Golf2021();
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Golf2021List(string StartDate="", string EndDate="", string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.Golf2021> lstGolf2021 = new List<Entities.Golf2021>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstGolf2021 = _Golf2021.GetGolf2021ListByVariable(StartDate, EndDate,Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstGolf2021 = lstGolf2021;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditGolf2021(Int64 Id)
        {
            try
            {
                int status = 0;
                objGolf2021 = _Golf2021.GetGolf2021ById(Id, ref status);
                if (status != -1 && objGolf2021 != null)
                {
                    ViewBag.objGolf2021 = objGolf2021;
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
        public ActionResult ViewGolf2021(Int64 Id)
        {
            try
            {
                int status = 0;
                objGolf2021 = _Golf2021.GetGolf2021ById(Id, ref status);
                if (status != -1 && objGolf2021 != null)
                {
                    ViewBag.objGolf2021 = objGolf2021;
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
        public ActionResult AddGolf2021()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertGolf2021(Entities.Golf2021 objGolf2021)
        {
            try
            {
                objGolf2021.created_at = DateTime.UtcNow;

                Int64 _status = _Golf2021.InsertGolf2021(objGolf2021);
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
                    return RedirectToAction("Index", "Golf2021");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Golf2021");
        }



        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Golf2021CommentUpdate(Entities.Golf2021 objGolf2021)
        {
            try
            {
                Int64 _status = _Golf2021.Golf2021CommentUpdate(objGolf2021);
                if (_status != -1)
                {

                    
                    if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Comments Successfully</div>";
                    }
                    return RedirectToAction("Index", "Golf2021");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Golf2021");
        }


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Golf2021.DeleteGolf2021(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Golf2021.</div>";
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



        public void Golf2021ExporttoExcel(string Search = "",  string StartDate = "", string EndDate = "", string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DGolf2021.ExportGolf2021List(Search, StartDate, EndDate,Sort);
              
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "Golf2021-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Golf2021-Registration-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
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
