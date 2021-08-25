using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
      [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Members,")]
    public class MembersOrdersController : Controller
    {
        BLL.Members _Members = new BLL.Members();
        List<Entities.Members> lstMembers = new List<Entities.Members>();
        Entities.Members objMembers = new Entities.Members();
        BLL.MembershipTypes _MembershipType = new BLL.MembershipTypes();
        List<Entities.MembershipTypes> lstMembershipType = new List<Entities.MembershipTypes>();

        
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index(Int64 MembershipTypeId = 0)
        {
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult MembersOrdersList(string Search = "", string SortColumn = "InsertedTime", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;
            try
            {
                lstMembers = _Members.GetMembersOrderDetailsListByVariable(Search, Sort, PageNo, Items, ref Total);               
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstMembers = lstMembers;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditMemberOrder(Int64 MemberOrderId = 0)
        {
            try
            {
                int qstatus = 0;
                objMembers = _Members.AddMembershipRequirement(ref qstatus);

                if (qstatus == 1)
                {
                    ViewBag.objMembers = objMembers;
                    ViewBag.status = qstatus;
                }
                int _qstatus = 0;
                Entities.MembershipOrders _objMemberOrder = _Members.GetMemberOrderById(MemberOrderId, ref _qstatus);

                if (_qstatus == 1 && qstatus == 1)
                {
                    ViewBag.objMemberOrder = _objMemberOrder;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Members");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Members");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateMemberOrder(Entities.MembershipOrders objMemberOrder)
        {
            try
            {
                objMemberOrder.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                Int64 _status = _Members.InsertMemberOrder(objMemberOrder);
                if (_status != -1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Order Successfully</div>";
                    return RedirectToAction("EditMemberOrder", "MembersOrders", new { MemberOrderId = objMemberOrder.MembershipOrderId });

                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction.</div>";
                    return RedirectToAction("EditMemberOrder", "MembersOrders", new { MemberOrderId = objMemberOrder.MembershipOrderId });
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("EditMemberOrder", "MembersOrders", new { MemberOrderId = objMemberOrder.MembershipOrderId });
            }

        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewMemberOrder(Int64 MemberOrderId = 0)
        {
            try
            {
                int _qstatus = 0;
                Entities.Members _objMembers = _Members.GetMemberFullDetailsById(MemberOrderId, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.objMemberDetails = _objMembers;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "Members");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "Members");
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeleteMemberOrder(Int64 MemberOrderId)
        {
            string str = "";
            try
            {
                Int64 _status = _Members.DeleteMemberOrder(MemberOrderId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting order</div>";
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
