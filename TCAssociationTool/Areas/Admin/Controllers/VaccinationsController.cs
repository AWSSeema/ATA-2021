using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class VaccinationsController : Controller
    {
        BLL.Vaccinations _Vaccinations = new BLL.Vaccinations();
        Entities.Vaccinations objVaccinations = new Entities.Vaccinations();

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult VaccinationsList(string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.Vaccinations> lstVaccinations = new List<Entities.Vaccinations>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstVaccinations = _Vaccinations.GetVaccinationsListByVariable(Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstVaccinations = lstVaccinations;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditVaccinations(Int64 Id)
        {
            try
            {
                int status = 0;
                objVaccinations = _Vaccinations.GetVaccinationsById(Id, ref status);
                if (status != -1 && objVaccinations != null)
                {
                    ViewBag.objVaccinations = objVaccinations;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed processing your request.</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewVaccinations(Int64 Id)
        {
            try
            {
                int status = 0;
                objVaccinations = _Vaccinations.GetVaccinationsById(Id, ref status);
                if (status != -1 && objVaccinations != null)
                {
                    ViewBag.objVaccinations = objVaccinations;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed</div>";
                }

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult AddVaccinations()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertVaccinations(Entities.Vaccinations objVaccinations)
        {
            try
            {
               
                
                Int64 _status = _Vaccinations.InsertVaccinations(objVaccinations);
                if (_status != -1)
                {

                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                    }
                    else if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "Vaccinations");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Vaccinations");
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult VaccinationsCommentUpdate(Entities.Vaccinations objVaccinations)
        {
            try
            {


                Int64 _status = _Vaccinations.VaccinationsCommentUpdate(objVaccinations);
                if (_status != -1)
                {

                     if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Comment Successfully</div>";
                    }
                    return RedirectToAction("Index", "Vaccinations");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Vaccinations");
        }


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Vaccinations.DeleteVaccinations(Id);
                switch (_status)
                {
                    case 1:
                        

                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Vaccinations.</div>";
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


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult VaccinationsDeleteAll(string Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Vaccinations.VaccinationsDeleteAll(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Vaccinations.</div>";
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


    }
}
