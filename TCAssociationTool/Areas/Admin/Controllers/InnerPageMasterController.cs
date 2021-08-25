using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,InnerPages,SubAdmin,")]
    public class InnerPageMasterController : Controller
    {
        BLL.InnerPageCategories _InnerPageCategories = new BLL.InnerPageCategories();
        List<Entities.InnerPageCategories> lstInnerPageCategory = new List<Entities.InnerPageCategories>();
         
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        { 
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult InnerPageCategoriesList()
        {
            try
            {
                int status = 0;
                List<Entities.InnerPageCategories> lstInnerPageCategories2 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories3 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories4 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories = _InnerPageCategories.GetInnerPageCategoriesAll(ref lstInnerPageCategories2, ref lstInnerPageCategories3, ref lstInnerPageCategories4, ref status);
                if (status == 1)
                {
                    ViewBag.lstInnerPageCategories = lstInnerPageCategories;
                    ViewBag.lstInnerPageCategories2 = lstInnerPageCategories2;
                    ViewBag.lstInnerPageCategories3 = lstInnerPageCategories3;
                    ViewBag.lstInnerPageCategories4 = lstInnerPageCategories4;
                    ViewBag.total = lstInnerPageCategories.Count;
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            } 
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult InnerPageCategoriesStatus(Int64 InnerPageCategoryId)
        {
            string str = "";
            string message = "";

            try
            {
                Int64 _status = _InnerPageCategories.UpdateInnerPageCategoriesStatus(InnerPageCategoryId);
                if (_status == 1)
                {
                    message = "success";
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Menu Item status successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    message = "error";
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating Menu Item status</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                message = "error";
                return Json(new { ok = true, data = str });
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult CreateInnerPageCategories()
        {
            try
            {
                List<Entities.InnerPageCategories> lstInnerPageCategories2 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories3 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories4 = new List<Entities.InnerPageCategories>();
                int status = 0;
                List<Entities.InnerPageCategories> lstInnerPageCategories = _InnerPageCategories.GetInnerPageCategoriesAll(ref lstInnerPageCategories2, ref lstInnerPageCategories3, ref lstInnerPageCategories4, ref status);
                if (status == 1)
                { 
                    ViewBag.lstInnerPageCategories = lstInnerPageCategories;
                    ViewBag.lstInnerPageCategories2 = lstInnerPageCategories2;
                    ViewBag.lstInnerPageCategories3 = lstInnerPageCategories3;
                    ViewBag.lstInnerPageCategories4 = lstInnerPageCategories4;
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
            } 
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult CreateInnerPageCategories(Entities.InnerPageCategories objInnerPageCategories)
        {
            try
            {
                Int64 _status = 0;
                objInnerPageCategories.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                objInnerPageCategories.InsertedBy = HttpContext.User.Identity.Name.ToString();
                objInnerPageCategories.IsActive = true;
                _status = _InnerPageCategories.InsertInnerPageCategories(objInnerPageCategories);
                if (_status == 1 || _status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">" + (_status == 2 ? "Updated " : "Inserted ") + " Menu Item details successfully.</div>";
                    if (_status == 1)
                    {
                        return RedirectToAction("Index", "InnerPageMaster");
                    }
                    else
                    {
                        return RedirectToAction("EditInnerPageCategories", "InnerPageMaster", new { InnerPageCategoryId = objInnerPageCategories.InnerPageCategoryId });
                    }
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed " + (_status == 2 ? "updating " : "inserting ") + " Menu Item details.</div>";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return View();
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditInnerPageCategories(Int64 InnerPageCategoryId)
        {
            try
            {
                int _status = 0;
                int _list = 0;
                Entities.InnerPageCategories objInnerPageCategories = _InnerPageCategories.GetInnerPageCategoriesById(InnerPageCategoryId, ref _status);
                List<Entities.InnerPageCategories> lstInnerPageCategories2 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories3 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories4 = new List<Entities.InnerPageCategories>();
                List<Entities.InnerPageCategories> lstInnerPageCategories = _InnerPageCategories.GetInnerPageCategoriesAll(ref lstInnerPageCategories2, ref lstInnerPageCategories3, ref lstInnerPageCategories4, ref _list);
                if (_list == 1)
                {
                    ViewBag.lstInnerPageCategories = lstInnerPageCategories;
                    ViewBag.lstInnerPageCategories2 = lstInnerPageCategories2;
                    ViewBag.lstInnerPageCategories3 = lstInnerPageCategories3;
                    ViewBag.lstInnerPageCategories4 = lstInnerPageCategories4;
                }
                if (_status == 1)
                { 
                    ViewBag.objInnerPageCategories = objInnerPageCategories;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "InnerPageMaster");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "InnerPageMaster");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult EditInnerPageCategories(Entities.InnerPageCategories objInnerPageCategories)
        {
            try
            {
                Int64 _status = 0;
                objInnerPageCategories.IsActive = Convert.ToBoolean(ConfigurationManager.AppSettings["masterstatus"]);

                _status = _InnerPageCategories.InsertInnerPageCategories(objInnerPageCategories);
                if (_status == 1 || _status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">" + (_status == 2 ? "Updated " : "Inserted ") + " Menu Item details successfully.</div>";
                    return RedirectToAction("Index", "InnerPageMaster");
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed " + (_status == 2 ? "updating " : "inserting ") + " Menu Item details.</div>";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return View();
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult ViewInnerPageCategories(Int64 InnerPageCategoryId)
        {
            try
            {
                int _status = 0;
                Entities.InnerPageCategories objInnerPageCategories = _InnerPageCategories.GetInnerPageCategoriesById(InnerPageCategoryId, ref _status);
                if (_status == 1)
                {
                    ViewBag.objInnerPageCategories = objInnerPageCategories;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "InnerPageMaster");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                return RedirectToAction("Index", "InnerPageMaster");
            }
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult InnerPageCategoriesDelete(Int64 InnerPageCategoryId)
        {
            string str = "";
            string message = "";

            try
            {
                Int64 _status = _InnerPageCategories.DeleteInnerPageCategories(InnerPageCategoryId);
                if (_status == 1)
                {
                    message = "success";
                    str = "<div class=\"alert alert-success alert-dismissable\">Deleted Menu Item successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    message = "error";
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting Menu Item </div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch (Exception ex)
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">" + ex.Message + "</div>";
                message = "error";
                return Json(new { ok = true, data = str });
            }
        }

        [HttpPost]
        public JsonResult UpdateInnerPageCategoryDisplayOrder(int Position, Int64 InnerPageCategoryId)
        {
            string str = "";
            try
            {
                Int64 _status = _InnerPageCategories.UpdateInnerPageCategoryDisplayOrder(Position, InnerPageCategoryId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Updated Order No Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed updating Order No</div>";
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
