using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class CategoriesController : Controller
    {
        BLL.SubCategories _SubCategories = new BLL.SubCategories();
        BLL.Categories _Categories = new BLL.Categories();
        List<Entities.Categories> lstCategories = new List<Entities.Categories>();

        #region Categories

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index(Int64 id = 0)
        {
            int _qstatus = 0;
            ViewBag.lstCategories = _SubCategories.GetSubCategoryById(id,ref _qstatus);

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult CategoriesList(string Search = "", string SortColumn = "datemodified", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstCategories = _Categories.GetCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstCategories = lstCategories;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddCategories(Entities.Categories objCategories)
        {
            string str = "";
            bool _bool = true;
            try
            {

                Int64 _status = _Categories.InsertCategories(objCategories);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Category details successfully.</div>";
                        return RedirectToAction("Index", "Categories");
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Category details successfully.</div>";
                        return RedirectToAction("Index", "Categories");
                    case 3:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"> '" + objCategories.catname + "' is already existed.</div>";
                        return RedirectToAction("Index", "Categories");
                    case -1:
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                        return RedirectToAction("Index", "Categories");
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
        public ActionResult EditCategories(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Categories _objCategories = _Categories.GetCategoryById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objCategories });
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
        public ActionResult ViewCategories(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Categories _objCategories = _Categories.GetCategoryById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objCategories });
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
        public JsonResult DeleteCategories(Int64 id)
        {
            string str = "";
            try
            {

                Int64 _status = _Categories.DeleteCategories(id);
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
