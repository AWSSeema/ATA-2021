using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        BLL.NewsLetter _NewsLetter = new BLL.NewsLetter();
        BLL.AppInfo _AppInfo = new BLL.AppInfo();
        BLL.Enquiries _Enquiries = new BLL.Enquiries();
        BLL.MailTemplates _MailTemplates = new BLL.MailTemplates();
        BLL.Members _Members = new BLL.Members();
        Entities.Members _objMember = new Entities.Members();
        TCAssociationTool.BLL.AppInfo _appinfo = new BLL.AppInfo();
        BLL.Events _Events = new BLL.Events();
        Entities.Enquiries objEnquiriy = new Entities.Enquiries();
        TCAssociationTool.BLL.Enquiries _Enquiry = new TCAssociationTool.BLL.Enquiries();
        Entities.Events objEvents = new Entities.Events();
        List<Entities.EventCategories> lstEventCategories = new List<Entities.EventCategories>();
        List<Entities.EventRegistrationTypes> lstEventRegistrationTypes = new List<Entities.EventRegistrationTypes>();
        BLL.InnerPages _InnerPages = new BLL.InnerPages(); 
         
        public ActionResult Index(string str = "", string type = "", string ttype = "")
        {
            List<Entities.News> lstNews = new List<Entities.News>();
            Entities.InnerPages objPInnerPages = new Entities.InnerPages();
            List<Entities.WebsiteBanners> lstWebsiteBanners = new List<Entities.WebsiteBanners>();
            List<Entities.Events> lstUpcommingEvents = new List<Entities.Events>();
            Entities.InnerPages objPHInnerPages = new Entities.InnerPages();
           // Entities.InnerPages objCInnerPages = new Entities.InnerPages();
          //  List<Entities.Events> lstLatestEvents = new List<Entities.Events>();
            List<Entities.Sponsors> lstSponsors = new List<Entities.Sponsors>();
            List<Entities.Sponsors> lstMediaSponsors = new List<Entities.Sponsors>();
            //List<Entities.Photos> lstPhotos = new List<Entities.Photos>();
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();
            Entities.AppInfo objAppInfo = new Entities.AppInfo();
            List<Entities.SponsorCategories> lstSponsorCategories = new List<Entities.SponsorCategories>();
            Entities.Flyers objFlyers = new Entities.Flyers();
           // List<Entities.CommitteeCategories> lstCommitteeCategories = new List<Entities.CommitteeCategories>();
            List<Entities.InnerPageCategories> lstMenuItems = new List<Entities.InnerPageCategories>();
            List<Entities.InnerPageCategories> lstMenuItems2 = new List<Entities.InnerPageCategories>();
            List<Entities.InnerPageCategories> lstMenuItems3 = new List<Entities.InnerPageCategories>();
            List<Entities.InnerPageCategories> lstMenuItems4 = new List<Entities.InnerPageCategories>();
            List<Entities.InnerPageCategories> FooterMenuItems = new List<Entities.InnerPageCategories>();
            // List<Entities.Events> lstEvents = new List<Entities.Events>();
            Entities.InnerPages objSInnerPages = new Entities.InnerPages();
            List<Entities.Photos> lstPhotos = new List<Entities.Photos>();

            int status = 0;
            try
            {
                _AppInfo.FEGetListInitialLoad(ref lstNews, ref objPInnerPages, ref lstWebsiteBanners, ref objPHInnerPages, ref lstUpcommingEvents, ref lstSponsors, ref lstMediaSponsors, ref lstPhotos, ref lstVideos, ref objAppInfo, ref lstSponsorCategories, ref  objFlyers, ref lstMenuItems, ref lstMenuItems2, ref lstMenuItems3, ref lstMenuItems4, ref FooterMenuItems, ref status);

                if (type == "" && ttype == "")
                {
                    if (Request.Cookies["sval"] != null && Request.Cookies["sval"].Value == "home")
                    {
                        if (objFlyers != null && objFlyers.FlyerId != 0)
                        {
                            if (Request.Cookies["cval"] != null && Request.Cookies["cval"].Value == "home")
                            {
                                ViewBag.cval = "Yes";
                            }
                            else
                            {
                                Response.Cookies["cval"].Value = "home";
                                Response.Cookies["cval"].Expires = DateTime.Now.AddMinutes(10);
                                ViewBag.cval = "No";
                            }
                        }
                        ViewBag.sval = "yes";
                    }
                    else
                    {
                        Response.Cookies["sval"].Value = "home";
                        Response.Cookies["sval"].Expires = DateTime.Now.AddMinutes(30);
                        ViewBag.sval = "No";
                    }
                }
                else
                { 
                    ViewBag.cval = "yes";
                    ViewBag.sval = "yes";
                }
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstNews = lstNews;
            ViewBag.objPInnerPages = objPInnerPages;
            ViewBag.lstWebsiteBanners = lstWebsiteBanners;
            ViewBag.lstUpcommingEvents = lstUpcommingEvents;
            ViewBag.objPHInnerPages = objPHInnerPages;
            //ViewBag.objCInnerPages = objCInnerPages;
            //ViewBag.lstLatestEvents = lstLatestEvents;
            ViewBag.lstSponsors = lstSponsors;
            ViewBag.lstMediaSponsors = lstMediaSponsors;
            ViewBag.lstPhotos = lstPhotos;
            ViewBag.lstVideos = lstVideos;
            ViewBag.objAppInfo = objAppInfo;
            ViewBag.lstSponsorCategories = lstSponsorCategories;
            ViewBag.objFlyers = objFlyers;
           // ViewBag.lstCommitteeCategories = lstCommitteeCategories;
            ViewBag.lstMenuItems = lstMenuItems;
            ViewBag.lstMenuItems2 = lstMenuItems2;
            ViewBag.lstMenuItems3 = lstMenuItems3;
            ViewBag.lstMenuItems4 = lstMenuItems4;
            ViewBag.FooterMenuItems = FooterMenuItems;
           // ViewBag.objSInnerPages = objSInnerPages;
            // ViewBag.lstEvents = lstEvents;
              ViewBag.str = str;
            
            return View();
        }

        public ActionResult NewsLetterForm(string NEmail = "")
        {
            try
            {
                int status1 = 0;
                if (NEmail != "")
                {
                    Entities.NewsLetter objNewsLetter = new Entities.NewsLetter();
                    objNewsLetter.EmailId = NEmail;
                    Int64 status = 0;
                    status = _NewsLetter.InsertNewsLetter(objNewsLetter);
                    if (status == 1)
                    {
                        Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);
                        int _status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("User News Letter Subscriber", 0, ref _status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            //body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objNewsLetter.EmailId));
                            body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body.Replace("[SiteName]", objAppInfo.SiteName);

                            //BLL.Common.SendMail(objNewsLetter.EmailId, "Thank you for Subscribing your Email Id - ATA", body.ToString());
                        }
                        string msg = "<h4 class=\"clearfix m0 l-h12 font16 white-t OpenSans b-p10 t-c\">Acknowledgment</h4><article class=\"newssuccess newssuccess l-h20 yellow-t t-b-p10 t-c upasa-inner-bg p5 OpenSans\">Added your mailId to mailing list successfully.</article>";
                        return Json(new { ok = true, data = true, message = msg });

                    }
                    else
                    {
                        string msg = "<article class=\"newserror\">Failed to insert mailid.</article>";
                        return Json(new { ok = false, data = false, message = msg });
                    }
                }
                else
                {
                    string msg = "<article class=\"newserror\">Failed to insert mailid.</article>";
                    return Json(new { ok = false, data = false, message = msg });
                }
            }
            catch
            {
                string msg = "<article class=\"newserror\">Failed to insert mailid.</article>";
                return Json(new { ok = false, data = false, message = msg });
            }
        }

        public ActionResult ThankyouSubscription()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckEmailAvailabilityforNL(string Email)
        {
            try
            {
                int status = 0;
                Entities.NewsLetter objNewsLetter = _NewsLetter.GetNewsLetterByEmail(Email, ref status);
                bool data = (objNewsLetter != null && objNewsLetter.LetterId != 0 ? false : true);
                return Json(new { ok = true, data = data, message = "" });
            }
            catch
            {
                return Json(new { ok = false, message = "<div class=\"error closable\">Failed Transaction</div>" });
            }
        }

        #region Contact Us
        public ActionResult Feedback()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            Entities.InnerPages objInnerPageMap = new Entities.InnerPages();
            int status = 0;
            List<Entities.Events> lstEvents = _Events.GetEventsList(ref status);

            int status1 = 0;
            Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);

            objInnerPageMap = _InnerPages.GetInnerPagesListById(0, "Contact Us", ref status);

            int QStatus = 0;
            if (this.User.Identity.IsAuthenticated && this.User.Identity.Name != null && HttpContext.Session != null && Session["username"] != null)
            {
                _objMember = _Members.GetMemberFullDetailsByEmail(HttpContext.User.Identity.Name.ToString(), ref QStatus);
            }
            ViewBag.objAppInfo = objAppInfo;
            ViewBag.objInnerPageMap = objInnerPageMap;
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(Entities.Enquiries objEnquiry)
        {
            try
            {
                Int64 EnquiryId = 0;
                Int64 _status = _Enquiries.InsertEnquiries(objEnquiry, ref EnquiryId);

                int status1 = 0;
                Entities.AppInfo objAppInfo = _appinfo.GetAppInfoDetails(ref status1);

                if (_status == 1)
                {
                    if (objEnquiry.EventId != 0)
                    {
                        int status = 0;
                        Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("User Feedback - ATA", 0, ref status);
                        if (objTemplates != null)
                        {
                            StringBuilder body = new StringBuilder();
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEnquiry.Name));
                            body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body.Replace("[SiteName]", objAppInfo.SiteName);
                            body.Replace("[EventName]", (objEvents.EventName != null && objEvents.EventName != "") ? objEvents.EventName : "N/A");

                            //BLL.Common.SendMailwithfrom(objEnquiry.Email, objEvents.ContactEmail, "Thanks for your Comment - ATA", body.ToString());
                        }
                        int status2 = 0;
                        Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Feedback for Admin", 0, ref status2);
                        if (objTemplates1 != null)
                        {

                            StringBuilder body = new StringBuilder();
                            body.Append(objTemplates.Description);
                            body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                            body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEnquiry.Name));
                            body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                            body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                            body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                            body.Replace("[GUrl]", objAppInfo.SupportEmail);
                            body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                            body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                            body.Replace("[SiteName]", objAppInfo.SiteName);
                            body.Replace("[UserName]", BLL.Common.UppercaseFirst(objEnquiriy.Name));
                            body.Replace("[Email]", objEnquiriy.Email);
                            body.Replace("[Name]", BLL.Common.UppercaseFirst(objEnquiriy.Name));
                            body.Replace("[PhoneNo]", objEnquiriy.PhoneNo);
                            body.Replace("[Comments]", objEnquiriy.Description);
                            //body.Replace("[Subjects]", (objEnquiriy.Subject != null && objEnquiriy.Subject != "") ? objEnquiriy.Subject : "N/A");
                            body.Replace("[EventName]", (objEvents.EventName != null && objEvents.EventName != "") ? objEvents.EventName : "N/A");

                           // BLL.Common.SendMailwithfrom(objEnquiry.Email, objEvents.ContactEmail, "You have a new Feedback from ATA", body.ToString());
                        }
                        TempData["message"] = "<div class=\"success closable\">Sent your enquiry successfully</div>";
                        return RedirectToAction("Thankyou", "Home");
                    }
                    else
                    {
                        if (objEnquiry.Field1 == "General Enquiry")
                        {
                            int status = 0;
                            Entities.MailTemplates objTemplates = _MailTemplates.GetMailTemplateById("User Enquiry - ATA", 0, ref status);
                            if (objTemplates != null)
                            {
                                StringBuilder body = new StringBuilder();
                                body.Append(objTemplates.Description);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEnquiry.Name));
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[SiteName]", objAppInfo.SiteName);

                               // BLL.Common.SendMail(objEnquiry.Email, "Thank You for Contacting us - ATA", body.ToString());
                            }
                            int status2 = 0;
                            Entities.MailTemplates objTemplates1 = _MailTemplates.GetMailTemplateById("Enquiry for Admin", 0, ref status2);
                            if (objTemplates1 != null)
                            {

                                StringBuilder body = new StringBuilder();
                                body.Append(objTemplates.Description);
                                body.Replace("[usersiteurl]", ConfigurationManager.AppSettings["baseurl"].ToString());
                                body.Replace("[USERNAME]", BLL.Common.UppercaseFirst(objEnquiry.Name));
                                body.Replace("[FBUrl]", objAppInfo.FacebookUrl);
                                body.Replace("[TWUrl]", objAppInfo.TwitterUrl);
                                body.Replace("[YUrl]", objAppInfo.YoutubeUrl);
                                body.Replace("[GUrl]", objAppInfo.SupportEmail);
                                body.Replace("[TPhone]", objAppInfo.CompanyPhone);
                                body.Replace("[TEmail]", objAppInfo.CompanyEmail);
                                body.Replace("[SiteName]", objAppInfo.SiteName);
                                body.Replace("[UserName]", BLL.Common.UppercaseFirst(objEnquiriy.Name));
                                body.Replace("[Email]", objEnquiriy.Email);
                                body.Replace("[Name]", BLL.Common.UppercaseFirst(objEnquiriy.Name));
                                body.Replace("[Comments]", objEnquiriy.Description);
                                body.Replace("[Subjects]", (objEnquiriy.Subject != null && objEnquiriy.Subject != "") ? objEnquiriy.Subject : "N/A");
                                body.Replace("[Field1]", (objEnquiriy.Field1 != null && objEnquiriy.Field1 != "") ? objEnquiriy.Field1 : "N/A");

                               // BLL.Common.SendMail(objAppInfo.CompanyEmail, "You have a new Enquiry from ATA", body.ToString());
                            }
                        }

                       TempData["message"] = "<div class=\"success closable\">Sent your enquiry successfully</div>";
                       return RedirectToAction("Thankyou", "Home");

                    }
                }
                if (_status == 2)
                {
                    TempData["message"] = "<div class=\"success closable\">Updated Enquiry Details Successfully</div>";
                    return RedirectToAction("Thankyou", "Home");
                }
                else
                {
                    TempData["message"] = "<div class=\"error closable\">Failed uploading Enquiry Details.</div>";
                    return RedirectToAction("ContactUs", "Home");
                }
            }
            catch
            {
                TempData["message"] = "<div class=\"error closable\">Failed Transaction</div>";
                return RedirectToAction("ContactUs", "Home");
            }
        }

        public ActionResult Thankyou()
        {
            return View();
        }

        public void logreport(string error = "", string pageName = "", string filepath = "")
        {

            if (System.IO.File.Exists(filepath))
            {
                using (StreamWriter stwriter = new StreamWriter(filepath, true))
                {
                    stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
                    stwriter.WriteLine("Page :" + pageName);
                    stwriter.WriteLine(error);
                    stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
                }
            }
            else
            {
                StreamWriter stwriter = System.IO.File.CreateText(filepath);
                stwriter.WriteLine("-------------------START-------------" + DateTime.Now);
                stwriter.WriteLine("Page :" + pageName);
                stwriter.WriteLine(error);
                stwriter.WriteLine("-------------------END-------------" + DateTime.Now);
                stwriter.Close();
            }
        }

        #endregion

    }
}
