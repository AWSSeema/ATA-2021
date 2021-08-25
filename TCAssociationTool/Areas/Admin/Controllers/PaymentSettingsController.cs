using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCAssociationTool;

namespace TCAssociationTool.Areas.Admin.Controllers
{
     [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,")]
    public class PaymentSettingsController : Controller
    {
        BLL.PaymentSettings _PaymentSettings = new TCAssociationTool.BLL.PaymentSettings();
        Entities.PaymentSettings objPaymentSettings = new TCAssociationTool.Entities.PaymentSettings();

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult PartialList(string search = "",int PageNo = 1, int PageItems = 5)
        {
             int Total = 0;
             List<Entities.PaymentSettings> lstPaymentSettings = new List<Entities.PaymentSettings>();
            try
            {

                lstPaymentSettings = _PaymentSettings.GetPaymentSettingsList(HttpUtility.UrlDecode(search.Trim()), PageNo, PageItems, ref Total);
               
            }
            catch
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.pageno = PageNo;
            ViewBag.iterms = PageItems;
            ViewBag.total = Total;
            ViewBag.list = lstPaymentSettings;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Add(Int64 PaymentSettingId=0)
        {
            try
            {
                int status = 0;
                ViewBag.CurrencyCodesList = _PaymentSettings.GetCurrencyCodesList(ref status);
                ViewBag.PaymentMethodsList = _PaymentSettings.GetPaymentMethodsList(ref status);

                if (PaymentSettingId != 0)
                {
                    TCAssociationTool.Entities.PaymentSettings objPaymentSettings = _PaymentSettings.GetPaymentSettingsDetails(PaymentSettingId, "", ref status);
                    ViewBag.objPaymentSettings = objPaymentSettings;
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
           
            return View();
        }


        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult View(Int64 Id)
        {
            string str = "";
            int status = 0;
            try
            {
                TCAssociationTool.Entities.PaymentSettings objPaymentSettings = _PaymentSettings.GetPaymentSettingsDetails(Id, "", ref status);
                if (objPaymentSettings != null)
                {
                    return Json(new { ok = true, data = objPaymentSettings });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed</div>";
                    return Json(new { ok = false, data = str });
                }

            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        public ActionResult UpdateAppInfo(TCAssociationTool.Entities.PaymentSettings objPaymentSettings)
        {
            try
            {
                objPaymentSettings.PaymentPassword = "";
                Int64 status = _PaymentSettings.UpdatePaymentSettingsDetails(objPaymentSettings);
               
                if (status == 1)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                    return RedirectToAction("Index", "PaymentSettings");
                }
                if (status == 2)
                {
                    TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                    return RedirectToAction("Index", "PaymentSettings");
                }

                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed updating payment settings.</div>";
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Index", "PaymentSettings");
        }

        [HttpPost]
       [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public JsonResult Delete(Int64 Id)
        {
            string str = "";
            try
            {
                Int64 _status = _PaymentSettings.DeletePaymentSettingById(Id);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting item</div>";
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
        public JsonResult PaymentSettingstatus(Int64 PaymentSettingId)
        {
            string str = "";
            try
            {
                Int64 _status = _PaymentSettings.UpdatePaymentSettingApprovalById(PaymentSettingId);
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
