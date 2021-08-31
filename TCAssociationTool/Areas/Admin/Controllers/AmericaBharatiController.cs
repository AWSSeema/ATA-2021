using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,AmericaBharati,SubAdmin,")]
    public class AmericaBharatiController : Controller
    {
        BLL.AmericaBharati _AmericaBharati = new BLL.AmericaBharati();
        List<Entities.AmericaBharati> lstAmericaBharati = new List<Entities.AmericaBharati>();
       

        #region AmericaBharati

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {

          return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult DetailsList(string Search = "", string SortColumn = "datemodified", string SortOrder = "DESC", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstAmericaBharati = _AmericaBharati.GetAmericaBharatiListByVariable(Search, Sort, PageNo, Items, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstAmericaBharati = lstAmericaBharati;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddDetails()
        {
           return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddDetails(Entities.AmericaBharati objAmericaBharati)
        {
            try
            {
               
                objAmericaBharati.status2 = true;
                Int64 _status = _AmericaBharati.InsertAmericaBharati(objAmericaBharati);
                if (_status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Index", "AmericaBharati");
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("EditDetails", "AmericaBharati", new { id = objAmericaBharati.id });
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading page.</div>";
                    return RedirectToAction("AddDetails", "AmericaBharati");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("AddDetails", "AmericaBharati");
            }

        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditDetails(Int64 id = 0)
        {
            try
            {

                int _qstatus = 0;
                Entities.AmericaBharati _objAmericaBharati = _AmericaBharati.GetAmericaBharatiById(id, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.objDetails = _objAmericaBharati;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\" alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "AmericaBharati");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "AmericaBharati");
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewDetails(Int64 InnerPageId = 0)
        {
            try
            {

                int _qstatus = 0;
                Entities.AmericaBharati _objAmericaBharati = _AmericaBharati.GetAmericaBharatiById(InnerPageId, ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.objDetails = _objAmericaBharati;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "AmericaBharati");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Index", "AmericaBharati");
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeleteDetails(Int64 id)
        {
            string str = "";
            try
            {

                Int64 _status = _AmericaBharati.DeleteAmericaBharati(id);
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
        public JsonResult AmericaBharatitatus(Int64 id)
        {
            string str = "";
            try
            {
                Int64 _status = _AmericaBharati.UpdateAmericaBharatiStatus(id);
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


        #endregion

    }
}
