using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class VolunteerCategoriesController : Controller
    {
        BLL.VolunteerCategories _VolunteerCategory = new BLL.VolunteerCategories();
        List<Entities.VolunteerCategories> lstVolunteerCategory = new List<Entities.VolunteerCategories>();

        #region VolunteerCategories

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult VolunteerCategoriesList(string Search = "", string SortColumn = "OrderNo", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstVolunteerCategory = _VolunteerCategory.GetVolunteerCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstVolunteerCategory = lstVolunteerCategory;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddVolunteerCategory(Entities.VolunteerCategories objVolunteerCategory)
        {
            string str = "";
            bool _bool = true;
            try
            {

                Int64 _status = _VolunteerCategory.InsertVolunteerCategories(objVolunteerCategory);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Category details successfully.</div>";
                        return RedirectToAction("Index", "VolunteerCategories");
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Category details successfully.</div>";
                        return RedirectToAction("Index", "VolunteerCategories");
                    case 3:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"> '" + objVolunteerCategory.CategoryName + "' is already existed.</div>";
                        return RedirectToAction("Index", "VolunteerCategories");
                    case -1:
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                        return RedirectToAction("Index", "VolunteerCategories");
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
        public ActionResult EditVolunteerCategory(Int64 VolunteerCategoryId = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.VolunteerCategories _objVolunteerCategory = _VolunteerCategory.GetVolunteerCategoryById(VolunteerCategoryId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objVolunteerCategory });
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
        public ActionResult ViewVolunteerCategory(Int64 VolunteerCategoryId = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.VolunteerCategories _objVolunteerCategory = _VolunteerCategory.GetVolunteerCategoryById(VolunteerCategoryId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objVolunteerCategory });
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
        public JsonResult DeleteVolunteerCategory(Int64 VolunteerCategoryId)
        {
            string str = "";
            try
            {

                Int64 _status = _VolunteerCategory.DeleteVolunteerCategory(VolunteerCategoryId);
                if (_status == 1)
                {

                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Donation Category</div>";
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
        public JsonResult VolunteerCategoryStatus(Int64 VolunteerCategoryId)
        {
            string str = "";
            try
            {

                Int64 _status = _VolunteerCategory.UpdateVolunteerCategoryStatus(VolunteerCategoryId);
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

        [HttpPost]
        public JsonResult VolunteerCategoryDisplayOrder(int OrderNo, Int64 VolunteerCategoryId)
        {
            string str = "";
            try
            {
                Int64 _status = _VolunteerCategory.UpdateVolunteerCategoryDisplayOrder(OrderNo, VolunteerCategoryId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Order Successfully</div>";
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

        #endregion  

    }
}
