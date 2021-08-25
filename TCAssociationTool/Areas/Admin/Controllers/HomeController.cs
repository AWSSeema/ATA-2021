using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
    [Areas.Admin.Models.SessionClass.SessionExpireFilter]
    public class HomeController : Controller
    {
        BLL.MailTemplates _MailTemplate = new BLL.MailTemplates();
        BLL.Members _Members = new BLL.Members();
        Entities.Enquiries objEnquiries = new Entities.Enquiries();
        Entities.Members objMembers = new Entities.Members();
        BLL.Enquiries _Enquiries = new BLL.Enquiries();
        Entities.EventUserInfo objEventUserInfo = new Entities.EventUserInfo();
        BLL.Events _Events = new BLL.Events();
        List<Entities.MailTemplates> lstMailTemplate = new List<Entities.MailTemplates>();
        Entities.Donors objDonors = new Entities.Donors();
        BLL.Donors _Donors = new BLL.Donors(); 
        Entities.Volunteers objVolunteers = new Entities.Volunteers();
        BLL.Volunteers _Volunteers = new BLL.Volunteers();
        Entities.NewsLetter objNewsLetter = new Entities.NewsLetter();
        BLL.NewsLetter _NewsLetter = new BLL.NewsLetter();

        BLL.AppInfo _AppInfo = new BLL.AppInfo();

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult SendMail(Int64 EnquiryId = 0, Int64 MemberId = 0, Int64 DonorId = 0, Int64 EventUserInfoId = 0, Int64 AdvertiseWithUsId = 0, Int64 VolunteerId = 0, Int64 LetterId = 0)
        {
            try
            {
                int _qstatus = 0;
                string Email = "";
                
                if (EnquiryId != 0)
                {
                    objEnquiries = _Enquiries.GetEnquirysById(EnquiryId, ref _qstatus);
                    Email = objEnquiries.Email;
                }
                if (MemberId != 0)
                {
                    objMembers = _Members.GetMemberFullDetailsById(MemberId, ref _qstatus);
                    Email = objMembers.Email;
                }
                if (EventUserInfoId != 0)
                {
                    objEventUserInfo = _Events.GetEventUserInfoById(EventUserInfoId, ref _qstatus);
                    Email = objEventUserInfo.Email;
                }
                if (DonorId != 0)
                {
                    objDonors = _Donors.GetDonorsById(DonorId, ref _qstatus);
                    Email = objDonors.Email;
                } 
                if (VolunteerId != 0)
                {
                    objVolunteers = _Volunteers.GetVolunteerById(VolunteerId, ref _qstatus);
                    Email = objVolunteers.Email;
                }
                if (LetterId != 0)
                {
                    objNewsLetter = _NewsLetter.GetNewsLetterById(LetterId);
                    Email = objNewsLetter.EmailId;
                }
                ViewBag.Email = Email;
                ViewBag.objEnquiries = objEnquiries;
                lstMailTemplate = _MailTemplate.GetMailTemplatesList("Manual", ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.lstMailTemplate = lstMailTemplate;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "MailTemplates");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Send(Entities.SendMail objSendMail)
        {
            try
            {
                BLL.Common.SendMail(objSendMail.EmailTo, objSendMail.Subject, objSendMail.Description.ToString());
                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Sent mail sucessfully.</div>";
            }
            catch (Exception ex)
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">"+ex.Message+"</div>";
            }
            return RedirectToAction("SendMail", "Home");
        }

        public ActionResult MailTemplate(string MailTemplateName = "")
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                Entities.MailTemplates objMailTemplate = _MailTemplate.GetMailTemplateById(MailTemplateName, 0, ref _qstatus);

                if (objMailTemplate != null)
                {
                    return Json(new { ok = true, data = objMailTemplate });
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

    }
}
