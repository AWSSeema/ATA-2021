using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class WomensdayLocationsController : Controller
    {

        TCAssociationTool.BLL.WomensdayLocations _WomensdayLocations = new TCAssociationTool.BLL.WomensdayLocations();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult WomensdayLocationsList(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "id", string SortOrder = "DESC")
        {
            int Total = 0;
            List<Entities.WomensdayLocations> WomensdayLocationslist = new List<Entities.WomensdayLocations>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");

                WomensdayLocationslist = _WomensdayLocations.GetWomensdayLocationsListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.items = PageItems;
            ViewBag.pageno = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.WomensdayLocationslist = WomensdayLocationslist;
            return View();
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddWomensdayLocations(Entities.WomensdayLocations objWomensdayLocations)
        {
            try
            {

                objWomensdayLocations.datemodified = DateTime.UtcNow;
                objWomensdayLocations.status2 = false;
                Int64 _status = _WomensdayLocations.InsertWomensdayLocations(objWomensdayLocations);
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
                        return RedirectToAction("Index", "WomensdayLocations");

                    }

                return RedirectToAction("Index", "WomensdayLocations");

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "WomensdayLocations");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult EditWomensdayLocations(Int64 id)
        {
            string str = "";
            int status = 0;
            try
            {
                Entities.WomensdayLocations objWomensdayLocations = _WomensdayLocations.GetWomensdayLocationsById(id, ref status);
                if (objWomensdayLocations != null)
                {
                    return Json(new { ok = true, data = objWomensdayLocations });
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

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]

        public JsonResult DeleteWomensdayLocations(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _WomensdayLocations.DeleteWomensdayLocations(id);
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

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult WomensdayLocationsStatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _WomensdayLocations.WomensdayLocationsCommentUpdate(id);
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




    }
}
