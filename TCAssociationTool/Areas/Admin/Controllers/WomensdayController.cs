using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    public class WomensdayController : Controller
    {
        BLL.Womensday _Womensday = new BLL.Womensday();
        Entities.Womensday objWomensday = new Entities.Womensday();

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult WomensdayList(string location="",string Search = "", string SortColumn = "Id", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            List<Entities.Womensday> lstWomensday = new List<Entities.Womensday>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
                lstWomensday = _Womensday.GetWomensdayListByVariable(location,Search, Sort, PageNo, Items, ref Total);
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.total = Total;
            ViewBag.lstWomensday = lstWomensday;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult EditWomensday(Int64 Id)
        {
            try
            {
                int status = 0;
                List<Entities.Womensdayguests> lstWomensdayguests = new List<Entities.Womensdayguests>();

                objWomensday = _Womensday.GetWomensdayById(Id,ref  lstWomensdayguests, ref status);
                if (status != -1 && objWomensday != null)
                {
                    ViewBag.objWomensday = objWomensday;
                    ViewBag.lstWomensdayguests = lstWomensdayguests;
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
        public ActionResult ViewWomensday(Int64 Id)
        {
            try
            {
                int status = 0;
                List<Entities.Womensdayguests> lstWomensdayguests = new List<Entities.Womensdayguests>();

                objWomensday = _Womensday.GetWomensdayById(Id, ref lstWomensdayguests, ref status);
                if (status != -1 && objWomensday != null)
                {
                    ViewBag.objWomensday = objWomensday;
                    ViewBag.lstWomensdayguests = lstWomensdayguests;
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
        public ActionResult AddWomensday()
        {
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InsertWomensday(Entities.Womensday objWomensday)
        {
            try
            {


                Int64 _status = _Womensday.InsertWomensday(objWomensday);
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
                    return RedirectToAction("Index", "Womensday");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Womensday");
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult WomensdayCommentUpdate(Entities.Womensday objWomensday)
        {
            try
            {


                Int64 _status = _Womensday.WomensdayUpdateComments(objWomensday);
                if (_status != -1)
                {

                    if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Updated Comment Successfully</div>";
                    }
                    return RedirectToAction("Index", "Womensday");

                }
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "Womensday");
        }


        [HttpPost]
        [Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            bool _bool = true;

            try
            {
                Int64 _status = _Womensday.DeleteWomensday(Id);
                switch (_status)
                {
                    case 1:


                        str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Womensday.</div>";
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
