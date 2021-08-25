using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Committees,")]
    public class CommitteeMasterController : Controller
    {
        BLL.CommitteeCategories _CommitteeCategory = new BLL.CommitteeCategories();
        List<Entities.CommitteeCategories> lstCommitteeCategory = new List<Entities.CommitteeCategories>();

        #region CommitteeCategories

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
           
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult CommitteeCategoriesList(string Search = "", string SortColumn = "UpdatedTime", string SortOrder = "ASC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstCommitteeCategory = _CommitteeCategory.GetCommitteeCategoriesListByVariable(Search, Sort, PageNo, Items, ref Total);
                
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstCommitteeCategory = lstCommitteeCategory;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddCommitteeCategory(Entities.CommitteeCategories objCommitteeCategory)
        {
            string str = "";
            bool _bool = true;
            try
            {
               
                objCommitteeCategory.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _CommitteeCategory.InsertCommitteeCategories(objCommitteeCategory);
                switch (_status)
                {
                    case 1:
                        str = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        _bool = true;
                        break;
                    case 2:
                        str = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                    default:
                        str = "<div class=\"error-alert closable\">Failed processing your request.</div>";
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

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCommitteeCategory(Int64 CommitteeCategoryId = 0)
        {
            string str = "";
            try
            {
               
                int _qstatus = 0;
                Entities.CommitteeCategories _objCommitteeCategory = _CommitteeCategory.GetCommitteeCategoryById(CommitteeCategoryId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objCommitteeCategory });
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
        public JsonResult DeleteCommitteeCategory(Int64 CommitteeCategoryId)
        {
            string str = "";
            try
            {
               
                Int64 _status = _CommitteeCategory.DeleteCommitteeCategory(CommitteeCategoryId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting page</div>";
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
        public JsonResult CommitteeStatus(Int64 CommitteeCategoryId)
        {
            string str = "";
            try
            {
               
                Int64 _status = _CommitteeCategory.UpdateCommitteeCategoryStatus(CommitteeCategoryId);
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
        public JsonResult CommitteeCategoryDisplayOrder(int DisplayOrder, Int64 CommitteeCategoryId)
        {
            string str = "";
            try
            {
                Int64 _status = _CommitteeCategory.UpdateCommitteeCategoriesDisplayOrder(DisplayOrder, CommitteeCategoryId);
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
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        #endregion  

    }
}
