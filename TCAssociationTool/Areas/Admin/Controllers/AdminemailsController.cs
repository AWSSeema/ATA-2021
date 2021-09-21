using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class AdminemailsController : Controller
    {
        BLL.Adminemails _Adminemails = new BLL.Adminemails();
        List<Entities.Adminemails> lstAdminemails = new List<Entities.Adminemails>();

        #region Adminemails

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AdminemailsList(string Search = "", string SortColumn = "datemodified", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstAdminemails = _Adminemails.GetAdminemailsListByVariable(Search, Sort, PageNo, Items, ref Total);

            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstAdminemails = lstAdminemails;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();

            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddAdminemails(Entities.Adminemails objAdminemails)
        {
            string str = "";
            bool _bool = true;
            try
            {
                objAdminemails.datemodified = DateTime.UtcNow;
              
                Int64 _status = _Adminemails.InsertAdminemails(objAdminemails);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted  Record successfully.</div>";
                        return RedirectToAction("Index", "Adminemails");
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated  Record successfully.</div>";
                        return RedirectToAction("Index", "Adminemails");
                    case 3:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"> '" + objAdminemails.Id + "' is already existed.</div>";
                        return RedirectToAction("Index", "Adminemails");
                    case -1:
                    default:
                        TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                        return RedirectToAction("Index", "Adminemails");
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
        public ActionResult EditAdminemails(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Adminemails _objAdminemails = _Adminemails.GetAdminemailsById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objAdminemails });
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
        public ActionResult ViewAdminemails(Int64 id = 0)
        {
            string str = "";
            try
            {

                int _qstatus = 0;
                Entities.Adminemails _objAdminemails = _Adminemails.GetAdminemailsById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    return Json(new { ok = true, data = _objAdminemails });
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

                Int64 _status = _Adminemails.DeleteAdminemails(Id);
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
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult AdminemailsUpdatedefaultmembership(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Adminemails.AdminemailsUpdatedefaultmembership(id);
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
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult AdminemailsUpdatedefaultdonation(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Adminemails.AdminemailsUpdatedefaultdonation(id);
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
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult AdminemailsUpdateisdonation(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Adminemails.AdminemailsUpdateisdonation(id);
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
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult AdminemailsUpdateismembership(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _Adminemails.AdminemailsUpdateismembership(id);
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
        #endregion


    }
}
