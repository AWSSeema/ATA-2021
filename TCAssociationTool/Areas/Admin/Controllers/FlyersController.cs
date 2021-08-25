using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class FlyersController : Controller
    {
        TCAssociationTool.BLL.Flyers _Flyers = new TCAssociationTool.BLL.Flyers();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Flyerslist(string search = "", int PageNo = 1, int PageItems = 10, string SortColumn = "", string SortOrder = "")
        {
            int Total = 0;
            List<Entities.Flyers> Flyerslist = new List<Entities.Flyers>();
            try
            {
                string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");

                Flyerslist = _Flyers.GetFlyerListByVariable(HttpUtility.UrlDecode(search.Trim()), Sort, PageNo, PageItems, ref Total);

            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.items = PageItems;
            ViewBag.pageno = PageNo;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder;
            ViewBag.Flyerslist = Flyerslist;
            return View();
        }

        public ActionResult AddFlyers()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddFlyers(Entities.Flyers objFlyer, HttpPostedFileBase FlyerUrl)
        {
            try
            {
                FlyerUrl.SaveAs(ConfigurationManager.AppSettings["uploadPath"] + "\\Flyers\\NormalImages\\" + FlyerUrl.FileName);
                WebImage image = new WebImage(ConfigurationManager.AppSettings["uploadPath"] + "\\Flyers\\NormalImages\\" + FlyerUrl.FileName);
                string imageurl = (image != null ? image.ImageFormat : "NA");
                 
                objFlyer.UpdatedBy = this.User.Identity.Name;
                Int64 _status = _Flyers.InsertFlyers(objFlyer, ref imageurl);
                if (_status != -1)
                {
                    if (imageurl != "")
                    {
                        image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\Flyers\\NormalImages\\" + imageurl);
                        System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\Flyers\\NormalImages\\" + FlyerUrl.FileName);
                    }
                    if (_status == 1)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully.</div>";
                    }
                    else if (_status == 2)
                    {
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    }
                    return RedirectToAction("Index", "Flyers");

                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\"Uploading Failed.</div>";
                    return RedirectToAction("Index", "Flyers");
                }
            }
            catch(Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">"+ex.Message+"</div>";
                return RedirectToAction("Index", "Flyers");
            }
        }


        public ActionResult Edit(Int64 FlyerId)
        {
            int status = 0;

            Entities.Flyers objFlyers = _Flyers.GetFlyerById(FlyerId, ref status);

            ViewBag.objFlyers = objFlyers;
            return View();
        }


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult EditFlyer(Int64 FlyerId)
        {
            string str = "";
            int status = 0;
            try
            {
                Entities.Flyers objFlyers = _Flyers.GetFlyerById(FlyerId, ref status);
                if (objFlyers != null)
                {
                    return Json(new { ok = true, data = objFlyers });
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

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]

        public JsonResult DeleteFlyer(Int64 FlyerId, string FlyerUrl = "")
        {
            string str = "";
            try
            {
                Int64 _status = _Flyers.DeleteFlyer(FlyerId);
                if (_status == 1)
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + @"\\Flyers\\NormalImages\\" + FlyerUrl);
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting item</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch(Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">"+ex.Message+"</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult FlyersStatus(Int64 FlyerId)
        {
            string str = "";
            try
            {
                Int64 _status = _Flyers.UpdateFlyersStatus(FlyerId);
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


    }
}
