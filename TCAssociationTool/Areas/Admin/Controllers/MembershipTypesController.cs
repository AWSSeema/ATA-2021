using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
      [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Members,")]
    public class MembershipTypesController : Controller
    {
        BLL.MembershipTypes _MembershipTypes = new BLL.MembershipTypes();
        List<Entities.MembershipTypes> lstMembershipTypes = new List<Entities.MembershipTypes>();

        #region MembershipTypes

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult MembershipTypesList(string Search = "", string SortColumn = "Validity", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstMembershipTypes = _MembershipTypes.GetMembershipTypesListByVariable(Search, Sort, PageNo, Items, ref Total);
                
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstMembershipTypes = lstMembershipTypes;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddMembershipTypes(Entities.MembershipTypes objMembershipTypes)
        {
            string str = "";
            bool _bool = true;
            try
            {
                objMembershipTypes.UpdatedBy = this.User.Identity.Name;
                objMembershipTypes.IsActive = true;
                Int64 _status = _MembershipTypes.InsertMembershipType(objMembershipTypes);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        _bool = true;
                        break;
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                    default:
                        TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }

            return RedirectToAction("Index", "MembershipTypes");
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditMembershipTypes(Int64 MembershipTypeId = 0)
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                Entities.MembershipTypes _objMembershipTypes = _MembershipTypes.GetMembershipTypeById(MembershipTypeId, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objMembershipTypes });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
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
        public JsonResult DeleteMembershipTypes(Int64 MembershipTypeId)
        {
            string str = "";
            try
            {
                Int64 _status = _MembershipTypes.DeleteMembershipType(MembershipTypeId);
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


   

        public JsonResult UpdateDisplayOrder(int DisplayOrder, Int64 MembershipTypeId)
        {
            Int64 _status = 0;
            string str = "";
            _status = _MembershipTypes.UpdateMembershipTypeDisplayOrder(DisplayOrder, MembershipTypeId);
            if (_status == 1)
            {
                str = "<div class=\"alert alert-success alert-dismissable\">Update Order Successfully</div>";
                return Json(new { ok = true, data = str });
            }
            else
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }
            
        }

        public JsonResult UpdateMembershipTypeStatus(Int64 MembershipTypeId)
        {
            Int64 _status = 0;
            string str = "";
            _status = _MembershipTypes.UpdateMembershipTypeStatus(MembershipTypeId);
            if (_status == 1)
            {
                str = "<div class=\"alert alert-success alert-dismissable\">Update Status Successfully</div>";
                return Json(new { ok = true, data = str });
            }
            else
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed Transaction</div>";
                return Json(new { ok = false, data = str });
            }
            
        }


        #endregion  

    }
}
