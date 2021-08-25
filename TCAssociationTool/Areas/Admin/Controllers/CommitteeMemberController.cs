using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Committees,")]
    public class CommitteeMemberController : Controller
    {
        BLL.CommitteeMembers _CommitteeMembers = new BLL.CommitteeMembers();
        BLL.Committees _Committees = new BLL.Committees();
        List<Entities.Committees> lstCommittees = new List<Entities.Committees>();
        BLL.CommitteeCategories _CommitteeCategory = new BLL.CommitteeCategories();
        List<Entities.CommitteeCategories> lstCommitteeCategory = new List<Entities.CommitteeCategories>();       

        #region Committee Members

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index(Int64 CommitteeCategoryId = 0)
        {
            List<Entities.CommitteeMembers> _lstCommittee = new List<Entities.CommitteeMembers>();
            try
            {

                if (CommitteeCategoryId != 0)
                {
                    int _qstatus = 0;
                    _lstCommittee = _Committees.GetCommitteeMembersListById(CommitteeCategoryId, ref _qstatus);
                    lstCommitteeCategory= _CommitteeCategory.GetCommitteeCategoriesList(ref _qstatus);
                    if (_qstatus != 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                        return RedirectToAction("Index", "Committees");
                    }
                   
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.lstCommitteeCategory = lstCommitteeCategory;
            ViewBag.lstCommittee = _lstCommittee;
            ViewBag.CommitteeCategoryId = CommitteeCategoryId;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult CommitteeMemberList(Int64 CommitteeCategoryId = 0, string Search = "", string SortColumn = "DisplayOrder", string SortOrder = "Asc", int PageNo = 1, int Items = 20)
        {
           
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstCommittees = _CommitteeMembers.CommitteeMembersList(CommitteeCategoryId, Search, Sort, PageNo, Items, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstCommittees = lstCommittees;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddCommitteeMember(Entities.CommitteeMembers objCommitteeMember)
        {
            try
            {
               
                Int64 _status = _CommitteeMembers.InsertCommitteeMembers(objCommitteeMember);
                if (_status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Index", "CommitteeMember", new { CommitteeCategoryId = objCommitteeMember.CommitteeCategoryId });
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("Index", "CommitteeMember", new { CommitteeCategoryId = objCommitteeMember.CommitteeCategoryId });
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction.</div>";
                    return RedirectToAction("Index", "CommitteeMember", new { CommitteeCategoryId = objCommitteeMember.CommitteeCategoryId });
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "CommitteeMember", new { CommitteeCategoryId = objCommitteeMember.CommitteeCategoryId });
            }

        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditCommitteeMember(Int64 CommitteeMemberId = 0)
        {
            string str = "";
            try
            {
               
                int _qstatus = 0;
                Entities.CommitteeMembers _objCommitteeMember = _CommitteeMembers.GetCommitteeMemberById(CommitteeMemberId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objCommitteeMember });
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
        public JsonResult DeleteCommitteeMember(Int64 CommitteeMemberId)
        {
            string str = "";
            try
            {
               
                Int64 _status = _CommitteeMembers.DeleteCommitteeMember(CommitteeMemberId);
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
        public JsonResult CommitteeMemberDisplayOrder(int DisplayOrder, Int64 CommitteeMemberId)
        {
            string str = "";
            try
            {
                Int64 _status = _CommitteeMembers.UpdateCommitteeMemberDisplayOrder(DisplayOrder, CommitteeMemberId);
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
