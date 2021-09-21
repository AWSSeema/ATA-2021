using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class CouponsController : Controller
    {
        TCAssociationTool.DAL.Coupons _DCoupons = new TCAssociationTool.DAL.Coupons();
        TCAssociationTool.BLL.Coupons _Coupons = new TCAssociationTool.BLL.Coupons();
        List<Entities.Coupons> lstCoupons = new List<Entities.Coupons>();
        BLL.MembershipTypes _MembershipTypes = new BLL.MembershipTypes();
        List<Entities.MembershipTypes> lstMembershipTypes = new List<Entities.MembershipTypes>();


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            int _qstatus = 0;
            try
            {
                List<Entities.MembershipTypes> lstMembershipTypes = _MembershipTypes.GetMembershipTypesList(ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.lstMembershipTypes = lstMembershipTypes;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Coupons");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + ".</div>";
            }
            //ViewBag.MembershipTypeId = MembershipTypeId;

            return View();
        }
        [HttpGet]
        public ActionResult CouponsList(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "Id", string SortOrder = "DESC")
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {

                lstCoupons = _Coupons.GetCouponsListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }
            ViewBag.Total = Total;
            ViewBag.items = PageItems;
            ViewBag.PageNo = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.lstCoupons = lstCoupons;
            return View();
        }

      

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCoupons(Entities.Coupons objCoupons)
        {
            try
            {
                 objCoupons.status2 = true;
                objCoupons.datemodified = DateTime.UtcNow; ;

                Int64 _status = _Coupons.InsertCoupons(objCoupons);

                if (_status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Created Coupons Successfully</div>";
                    return RedirectToAction(("Index"), "Coupons");
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Coupons Successfully</div>";
                    return RedirectToAction("Index", "Coupons");
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading page.</div>";
                    return RedirectToAction("Index", "Coupons");
                }


            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + ".</div>";
                return RedirectToAction("Index", "Coupons");
            }

        }

        [TCAssociationTool.Models.SessionClass.SessionExpireFilter]
        public JsonResult DeleteCoupons(Int64 Id)
        {
            string str = "";
            try
            {
                Int64 _status = _Coupons.DeleteCoupons(Id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Successfully Deleted</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting item</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return Json(new { ok = false, data = str });
            }
        }


        [TCAssociationTool.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewCoupons(Int64 Id = 0)
        {
            try
            {

                int _qstatus = 0;
                Entities.Coupons _objCoupons = _Coupons.GetCouponsById(Id, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.objCoupons = _objCoupons;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Coupons");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + ".</div>";
                return RedirectToAction("Index", "Coupons");
            }
            return View();
        }


        [HttpPost]
        [TCAssociationTool.Models.SessionClass.SessionExpireFilter]
        public JsonResult CouponsStatus(Int64 Id)
        {
            string str = "";
            try
            {
                Int64 _status = _Coupons.UpdateCouponsStatus(Id);
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
      


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCoupons(Int64 Id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Coupons objCoupons = _Coupons.GetCouponsById(Id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = objCoupons });
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



    }
}
