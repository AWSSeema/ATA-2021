using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class SubCategoriesController : Controller
    {
        BLL.SubCategories _SubCategories = new BLL.SubCategories();
        List<Entities.SubCategories> lstSubCategories = new List<Entities.SubCategories>();
        BLL.Categories _Categories = new BLL.Categories();
        
        #region SubCategories

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index(Int64 catid = 0)
        {
           
            int _qstatus = 0;
            ViewBag.catlist = _Categories.GetCategoriesList(ref _qstatus);
           if(catid!=0)
            {
                lstSubCategories = _SubCategories.GetSubCategoriesList(catid, ref _qstatus);
            }
            ViewBag.lstSubCategories = lstSubCategories;
            ViewBag.catid = catid;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult SubCategoriesList(string Search = "",Int64 catid=0, string SortColumn = "datemodified", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstSubCategories = _SubCategories.GetSubCategoriesListByVariable(Search, catid, Sort, PageNo, Items, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstSubCategories = lstSubCategories;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddSubCategories(Entities.SubCategories objSubCategories)
        {
            string str = "";
            bool _bool = true;
            try
            {

                Int64 _status = _SubCategories.InsertSubCategories(objSubCategories);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Sub Category details successfully.</div>";
                        return RedirectToAction("Index", "SubCategories");
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Sub Category details successfully.</div>";
                        return RedirectToAction("Index", "SubCategories");
                    case 3:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"> '" + objSubCategories.scname + "' is already existed.</div>";
                        return RedirectToAction("Index", "SubCategories");
                    case -1:
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                        return RedirectToAction("Index", "SubCategories");
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
        public ActionResult EditSubCategories(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.SubCategories _objSubCategories = _SubCategories.GetSubCategoryById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objSubCategories });
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
        public ActionResult ViewSubCategories(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.SubCategories _objSubCategories = _SubCategories.GetSubCategoryById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objSubCategories });
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
        public JsonResult DeleteSubCategories(Int64 id)
        {
            string str = "";
            try
            {

                Int64 _status = _SubCategories.DeleteSubCategories(id);
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


        [HttpPost]
        public JsonResult SubCategoryDisplayOrder(int orderno, Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _SubCategories.UpdateSubCategoryDisplayOrder(orderno, id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Order Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed Updating Order</div>";
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
