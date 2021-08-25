using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
     [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class ApplicationSettingsController : Controller
    {
        BLL.AppInfo _appinfo = new BLL.AppInfo();
         
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            try
            {
                int status = 0;                
                Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status);
                if (status != 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.objAppInfo = objAppInfo;
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult UpdateAppInfo(Entities.AppInfo objAppInfo)
        {
            try
            {
                objAppInfo.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                Int64 status = _appinfo.UpdateAppInfoDetails(objAppInfo);
                if (status != -1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed updating application details.</div>";
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "ApplicationSettings");
        }

    }
}
