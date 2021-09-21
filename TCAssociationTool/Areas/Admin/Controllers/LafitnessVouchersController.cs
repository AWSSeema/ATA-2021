using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class LafitnessVouchersController : Controller
    {
         BLL.LafitnessVouchers _LafitnessVouchers = new BLL.LafitnessVouchers();
        List<Entities.LafitnessVouchers> lstLafitnessVouchers = new List<Entities.LafitnessVouchers>();

        #region LafitnessVouchers

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
           
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult LafitnessVouchersList(string Search = "", string SortColumn = "created_at", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstLafitnessVouchers = _LafitnessVouchers.GetLafitnessVouchersListByVariable(Search, Sort, PageNo, Items, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstLafitnessVouchers = lstLafitnessVouchers;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddLafitnessVouchers(Entities.LafitnessVouchers objLafitnessVouchers)
        {
            string str = "";
            bool _bool = true;
            try
            {
                objLafitnessVouchers.created_at = DateTime.UtcNow;
                objLafitnessVouchers.updated_at = DateTime.UtcNow;
                objLafitnessVouchers.status2 = true;

                Int64 _status = _LafitnessVouchers.InsertLafitnessVouchers(objLafitnessVouchers);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record details successfully.</div>";
                        return RedirectToAction("Index", "LafitnessVouchers");
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Record details successfully.</div>";
                        return RedirectToAction("Index", "LafitnessVouchers");
                    case 3:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"> '" + objLafitnessVouchers.member_id + "' is already existed.</div>";
                        return RedirectToAction("Index", "LafitnessVouchers");
                    case -1:
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                        return RedirectToAction("Index", "LafitnessVouchers");
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }

            return Json(new { ok = _bool, data = str });

        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditLafitnessVouchers(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.LafitnessVouchers _objLafitnessVouchers = _LafitnessVouchers.GetLafitnessVouchersById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objLafitnessVouchers });
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

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewLafitnessVouchers(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.LafitnessVouchers _objLafitnessVouchers = _LafitnessVouchers.GetLafitnessVouchersById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objLafitnessVouchers });
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

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            try
            {

                Int64 _status = _LafitnessVouchers.DeleteLafitnessVouchers(Id);
                if (_status == 1)
                {

                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Category</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }


        #endregion

    }
}
