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
    public class CulturalLocationsController : Controller
    {

        BLL.CulturalLocations _CulturalLocations = new BLL.CulturalLocations();
        Entities.CulturalLocations objCulturalLocations = new Entities.CulturalLocations();
        DAL.CulturalLocations _DCulturalLocations = new DAL.CulturalLocations();
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult CulturalLocationsList(string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.CulturalLocations> lstCulturalLocations = new List<Entities.CulturalLocations>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstCulturalLocations = _CulturalLocations.GetCulturalLocationsListByVariable(Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstCulturalLocations = lstCulturalLocations;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;

            return View();
        }

       

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCulturalLocations(Int64 Id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                objCulturalLocations = _CulturalLocations.GetCulturalLocationsById(Id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = objCulturalLocations });
                }
                else
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Failed Transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult AddCulturalLocations()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertCulturalLocations(Entities.CulturalLocations objCulturalLocations)
        {
            try
            {
                objCulturalLocations.datemodified = DateTime.UtcNow;

                Int64 _status = _CulturalLocations.InsertCulturalLocations(objCulturalLocations);
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
                    return RedirectToAction("Index", "CulturalLocations");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "CulturalLocations");
        }



     


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _CulturalLocations.DeleteCulturalLocations(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting CulturalLocations.</div>";
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
        [TCAssociationTool.Models.SessionClass.SessionExpireFilter]
        public JsonResult CulturalLocationsStatus(Int64 Id)
        {
            string str = "";
            try
            {
                Int64 _status = _CulturalLocations.CulturalLocationsStatus(Id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated status successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return Json(new { ok = false, data = str });
            }
        }



        public void CulturalLocationsExporttoExcel(string Search = "", string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            try
            {
                DataTable dtUni = _DCulturalLocations.ExportCulturalLocationsList(Search, Sort);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtUni, "CLocations-Registration-Export");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=CLocations-Registration-Export-" + DateTime.UtcNow.ToString("MM-dd-yyyy") + ".xlsx");
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
