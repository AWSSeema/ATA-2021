using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class CommitteeController : Controller
    {
        BLL.Committees _Committees = new BLL.Committees();
        BLL.CommitteeCategories _CommitteeCategories = new BLL.CommitteeCategories();
        List<Entities.Committees> lstCommittees = new List<Entities.Committees>();
        BLL.CommitteeMembers _CommitteeMembers = new BLL.CommitteeMembers();

        public ActionResult Index(string CType = "")
        {
            string Ctype = BLL.Common.EncodeURL(CType);
            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            List<Entities.CommitteeCategories> lstCommitteeCategories1 = new List<Entities.CommitteeCategories>();
            try
            {
                int status = 0;
                lstCommittees = _Committees.FECommitteesGetList(ref lstCommitteeCategories, CType, ref status);
                lstCommitteeCategories1 = _CommitteeCategories.GetCommitteeCategoriesList(ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstCommittees = lstCommittees;
            ViewBag.lstCommitteeCategories = lstCommitteeCategories;
            ViewBag.lstCommitteeCategories1 = lstCommitteeCategories1;
            ViewBag.Type = CType;
            return View();
        }

        public ActionResult PastPresidents(string CType = "", string Year = "")
        {
            List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            List<Entities.CommitteeCategories> lstCommitteeCategories1 = new List<Entities.CommitteeCategories>();
            try
            {
                int status = 0;
                lstCommittees = _Committees.FEPastCommitteesGetList(ref lstCommitteeCategories, CType, Year, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstCommittees = lstCommittees;
            ViewBag.lstCommitteeCategories = lstCommitteeCategories;
            ViewBag.Type = CType;
            ViewBag.Year = Year;
            return View();
        }

        public ActionResult Committeeinfo(Int64 CommitteeId = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Committees _objCommittees = _Committees.GetCommitteeById(CommitteeId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objCommittees });
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
